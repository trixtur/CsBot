using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CsBot.Command;
using CsBot.Games;
using CsBot.Interfaces;

namespace CsBot
{
	class CommandHandler
    {
        public IrcBotService IrcBotService { get; }

        /*private static string IrcBotService.Settings.nick = "Be|\\|der";
        private const string IrcBotService.Settings.command_start = "~";
        private static string IrcBotService.Settings.channels[0].Name = "#pyrous";
        private static string IrcBotService.Settings.channels[0].name2 = "#CsBot";
        private static string FromChannel = IrcBotService.Settings.channels[0].Name;
        */

        public string FromChannel { get; set; }
        public string Addresser { get; set; } = "";
        public Users Users { get; }

        Farkle farkle;
        readonly RssFeedCommand rssFeed;
        //readonly List<ICommand> Commands;
        //readonly List<IGame> Games;

        readonly Dictionary<string, ICommand> Commands2;
        readonly Dictionary<string, IGame> Games2;
        enum GamesList { Roll, RockPaperScissors };

        public CommandHandler(IrcBotService ircBotService)
        {
            IrcBotService = ircBotService;
            Users = new Users();
            rssFeed = new RssFeedCommand(this);

            //Commands = new List<ICommand>();
            //Games = new List<IGame>();

            Commands2 = RegisterAllCommands ();
            Games2 = RegisterAllGames ();

            //Commands.Add(new Insult(this));
            //Commands.Add(new Quote(this));
            //Commands.Add(new Praise(this));
            //Commands.Add(new APB(this));
            //Commands.Add(new Caffeine(this));
            //Commands.Add(new Say(this));
            //Commands.Add(new Emote(this));
            //Commands.Add(new StringReplace(this));

            //Games.Add(new Roll(this));
            //Games.Add(new RockPaperScissors(this));
        }

		Dictionary<string, ICommand> RegisterAllCommands ()
        {
	        var commands = Assembly.GetExecutingAssembly ()
		        .GetTypes ()
		        .Where (t => t.GetInterfaces ().Contains (typeof(ICommand)))
		        .Select (c => Activator.CreateInstance (c, this) as ICommand);

	        return commands.ToDictionary (command => command.Name);
        }

		Dictionary<string, IGame> RegisterAllGames ()
		{
			var games = Assembly.GetExecutingAssembly ()
				.GetTypes ()
				.Where (t => t.GetInterfaces ().Contains (typeof(IGame)))
				.Select (c => Activator.CreateInstance (c, this) as IGame);

			return games.ToDictionary (game => game.Name);
		}

		/// <summary>
		/// Make the Bot say something in a specific channel.
		/// </summary>
		/// <param Name="s">String to Say.</param>
		/// <param Name="c">Channel to talk in</param>
		public void Say(string s, string c)
        {
            if (s.StartsWith("/me"))
                s = $"{s.Replace("/me", "ACTION")}";
            if (s.EndsWith("me"))
                s = s.Replace(" me", $" {Addresser}");
            if (s.Contains(" {nick} "))
                s = s.Replace(" {nick} ", " me ");
            if (s.Contains(" me "))
                s = s.Replace(" me ", $" {Addresser} ");
            if (s.Contains($" {IrcBotService.Settings.Nick} "))
                s = s.Replace($" {IrcBotService.Settings.Nick} ", " me ");
            Console.WriteLine($"PRIVMSG {c} :{s}");
            IrcBotService.Writer.WriteLine($"PRIVMSG {c} :{s}");
            IrcBotService.Writer.Flush();
        }

        /// <summary>
        /// Make the Bot say something in a specific channel.
        /// </summary>
        /// <param Name="s">String to Say.</param>
        public void Say(string s)
        {
            if (s.StartsWith("/me"))
                s = $"{s.Replace("/me", "ACTION")}";
            if (s.EndsWith("me") || s.EndsWith("me"))
                s = s.Replace(" me", $" {Addresser}");
            if (s.Contains(" me "))
                s = s.Replace(" me ", $" {Addresser} ");
            if (s.Contains(" {nick} "))
                s = s.Replace(" {nick} ", " me ");
            if (s.Contains($" {IrcBotService.Settings.Nick} "))
                s = s.Replace($" {IrcBotService.Settings.Nick} ", " me ");
            Console.WriteLine($"PRIVMSG {FromChannel} :{s}");
            IrcBotService.Writer.WriteLine($"PRIVMSG {FromChannel} :{s}");
            IrcBotService.Writer.Flush();
        }

        public string GetChannel(string input)
        {
            var parts = input.Split(' ');
            return parts.FirstOrDefault(part => part.StartsWith("#"));
        }

        public void HandleMessage(string command, string fromChannel, string addresser)
        {
            Console.WriteLine($"Handling message: {command} : {fromChannel} : {addresser}");
            FromChannel = fromChannel;
            Addresser = addresser;
            var endCommand = command.IndexOf(" ") - 1;
            if (endCommand < 0)
                endCommand = command.Length - 1;
            var fixedCommand = command.Substring(1, endCommand);

            if (fixedCommand.StartsWith(IrcBotService.Settings.CommandStart))
            { //If present remove leading command, otherwise log it.
                fixedCommand = fixedCommand.TrimStart(IrcBotService.Settings.CommandStart.ToCharArray());
                command = command.TrimStart(IrcBotService.Settings.CommandStart.ToCharArray());
            }
            else
            {
                fixedCommand = "";
            }

            if (command.Length == endCommand + 1)
                if (fixedCommand.StartsWith("s"))
                {
                    fixedCommand = fixedCommand.Substring(1);
                    command = $"s {fixedCommand}";
                    endCommand = 1;
                }

            if (fixedCommand.StartsWith("1"))
            {
                fixedCommand = "say";
                command = command.Replace("1", $"say in {IrcBotService.Settings.Channels[0].Name}");
                endCommand = fixedCommand.Length;
            }

            if (fixedCommand.StartsWith("2"))
            {
                fixedCommand = "emote";
                command = command.Replace("2", $"emote in {IrcBotService.Settings.Channels[0].Name}");
                endCommand = fixedCommand.Length;
            }

            foreach (var c in Commands2.Values)
            {
                c.Handle(command, endCommand, fixedCommand);
            }

            foreach (var g in Games2.Values)
            {
                g.Play(command, endCommand, fixedCommand);
            }

            switch (fixedCommand)
            {
                case "insult":
                case "quote":
                case "praise":
                case "apb":
                case "caffeine":
                case "say":
                case "emote":
                case "roll":
                case "rps":
                case "s":

                    break;
                case "farklehelp":
                    farkle.Help();
                    break;
                case "farkleforfeit":
                    farkle.FarkleForfeit();
                    break;
                case "farklescore":
                    farkle.Score(command, endCommand);
                    break;
                case "farkle":
                    farkle.FarkleCommand(command, endCommand);
                    break;
                case "joinfarkle":
                    if (farkle == null)
                        farkle = new Farkle(this);
                    farkle.JoinFarkle();
                    break;
                case "getnext":
                    rssFeed.GetNext();
                    break;
                case "getmore":
                    rssFeed.GetMore(command, endCommand);
                    break;
                case "getfeeds":
                    rssFeed.GetFeeds(command, endCommand);
                    break;
                default:
                    Console.WriteLine($"\n{fixedCommand}");
                    break;
            }
        }

        public void UpdateUserName(string origUser, string newUser)
        {
            if (Users.HasUser(origUser))
            {
                var tempUser = Users[origUser];
                Users.Remove(origUser);
                Users.Add(newUser, tempUser);
                Console.WriteLine($"Updated username from {origUser} to {newUser}");
            }
            else if (newUser != IrcBotService.Settings.Nick)
            {
                Users.Add(newUser);
            }
        }

        public void ParseUsers(string usersInput)
        {
            Console.WriteLine($"Users Input {usersInput}");
            var users = usersInput.Substring(usersInput.LastIndexOf(":") + 1).Split(" ".ToCharArray());

            foreach (var user in users)
            {
                if (user == "") continue;

                if (user != IrcBotService.Settings.Nick && !Users.HasUser(user))
                {
                    if (user.StartsWith("@"))
                    {
                        Users.Add(user.Substring(1));
                        Console.WriteLine($"Found user {user.Substring(1)}");
                    }
                    else
                    {
                        Users.Add(user);
                        Console.WriteLine($"Found user {user}");
                    }
                }
            }

            Users.PrintUsers();
        }

        public void AddUser(string userName)
        {
            if (!Users.HasUser(userName))
                Users.Add(userName);
        }

        public void DirectRoShamBo(string choice)
        {
	        var rps = (RockPaperScissors) Games2[nameof(RockPaperScissors)];

            switch (choice)
            {
                case "rock":
		            rps.RPSValue(Addresser, (int)RoShamBo.Rock);
                    break;
                case "paper":
                    rps.RPSValue(Addresser, (int)RoShamBo.Paper);
                    break;
                case "scissors":
                    rps.RPSValue(Addresser, (int)RoShamBo.Scissors);
                    break;
                default:
                    Say($"/me whispers something to {Addresser}.");
                    Say("Valid options are either rock, paper, or scissors.", Addresser);
                    break;
            }

            HandleMessage($":{IrcBotService.Settings.CommandStart}rps", FromChannel, Addresser);
        }

        public void LastMessage(string user, string inputLine, string fromChannel)
        {
            if (Users.HasUser(user))
            {
                var message = inputLine.Substring(inputLine.LastIndexOf($"{fromChannel} :") + fromChannel.Length + 2);
                Users.AddUserLastMessage(user, message);
            }
        }

        public string GetAddresser()
        {
            return Addresser;
        }

        public IrcBotService GetIrcBot()
        {
            return IrcBotService;
        }

        public Users GetUsers()
        {
            return Users;
        }

        public string GetFromChannel()
        {
            return FromChannel;
        }

        public List<string> GetQuotes()
        {
            return IrcBotService.Settings.Quotes;
        }
    }
}
