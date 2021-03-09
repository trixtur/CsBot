using System;
using System.Collections.Generic;
using System.Linq;
using CsBot.Command;
using CsBot.Games;

namespace CsBot
{
    class CommandHandler
    {
        internal readonly IrcBot ircBot;
        public static readonly Random random = new Random();
        /*private static string ircBot.Settings.nick = "Be|\\|der";
        private const string ircBot.Settings.command_start = "~";
        private static string ircBot.Settings.channels[0].name = "#pyrous";
        private static string ircBot.Settings.channels[0].name2 = "#CsBot";
        private static string m_fromChannel = ircBot.Settings.channels[0].name;
        */
        string m_fromChannel;
        internal string m_addresser = "";
        internal readonly Users m_users;
        Farkle farkle;
        RssFeedCommand rssFeed;
        List<iCommand> Commands;
        List<iGame> Games;
        internal enum GamesList { Roll, RockPaperScissors };

        public CommandHandler(IrcBot ircBot)
        {
            this.ircBot = ircBot;
            m_users = new Users();
            rssFeed = new RssFeedCommand(this);

            Commands = new List<iCommand>();
            Games = new List<iGame>();

            Commands.Add(new Insult(this));
            Commands.Add(new Quote(this));
            Commands.Add(new Praise(this));
            Commands.Add(new APB(this));
            Commands.Add(new Caffeine(this));
            Commands.Add(new Say(this));
            Commands.Add(new Emote(this));
            Commands.Add(new StringReplace(this));

            Games.Add(new Roll(this));
            Games.Add(new RockPaperScissors(this));
        }

        /// <summary>
        /// Make the Bot say something in a specific channel.
        /// </summary>
        /// <param name="s">String to Say.</param>
        /// <param name="c">Channel to talk in</param>
        public void Say(string s, string c)
        {
            if (s.StartsWith("/me"))
                s = "\x1" + s.Replace("/me", "ACTION") + "\x1";
            if (s.EndsWith("me"))
                s = s.Replace(" me", " " + m_addresser);
            if (s.Contains(" {nick} "))
                s = s.Replace(" {nick} ", " me ");
            if (s.Contains(" me "))
                s = s.Replace(" me ", " " + m_addresser + " ");
            if (s.Contains(" " + ircBot.Settings.nick + " "))
                s = s.Replace(" " + ircBot.Settings.nick + " ", " me ");
            Console.WriteLine("PRIVMSG " + c + " :" + s);
            ircBot.Writer.WriteLine("PRIVMSG " + c + " :" + s);
            ircBot.Writer.Flush();
        }

        /// <summary>
        /// Make the Bot say something in a specific channel.
        /// </summary>
        /// <param name="s">String to Say.</param>
        public void Say(string s)
        {
            if (s.StartsWith("/me"))
                s = "\x1" + s.Replace("/me", "ACTION") + "\x1";
            if (s.EndsWith("me") || s.EndsWith("me"))
                s = s.Replace(" me", " " + m_addresser);
            if (s.Contains(" me "))
                s = s.Replace(" me ", " " + m_addresser + " ");
            if (s.Contains(" {nick} "))
                s = s.Replace(" {nick} ", " me ");
            if (s.Contains(" " + ircBot.Settings.nick + " "))
                s = s.Replace(" " + ircBot.Settings.nick + " ", " me ");
            Console.WriteLine("PRIVMSG " + m_fromChannel + " :" + s);
            ircBot.Writer.WriteLine("PRIVMSG " + m_fromChannel + " :" + s);
            ircBot.Writer.Flush();
        }

        public string GetChannel(string input)
        {
            var parts = input.Split(' ');
            return parts.FirstOrDefault(part => part.StartsWith("#"));
        }

        public void HandleMessage(string command, string fromChannel, string addresser)
        {
            Console.WriteLine("Handling message: " + command + " : " + fromChannel + " : " + addresser);
            m_fromChannel = fromChannel;
            m_addresser = addresser;
            int endCommand = command.IndexOf(" ") - 1;
            if (endCommand < 0)
                endCommand = command.Length - 1;
            string fixedCommand = command.Substring(1, endCommand);

            if (fixedCommand.StartsWith(ircBot.Settings.command_start))
            { //If present remove leading command, otherwise log it.
                fixedCommand = fixedCommand.TrimStart(ircBot.Settings.command_start.ToCharArray());
                command = command.TrimStart(ircBot.Settings.command_start.ToCharArray());
            }
            else
            {
                fixedCommand = "";
            }

            if (command.Length == endCommand + 1)
                if (fixedCommand.StartsWith("s"))
                {
                    fixedCommand = fixedCommand.Substring(1);
                    command = "s " + fixedCommand;
                    endCommand = 1;
                }

            if (fixedCommand.StartsWith("1"))
            {
                fixedCommand = "say";
                command = command.Replace("1", "say in " + ircBot.Settings.channels[0].name);
                endCommand = fixedCommand.Length;
            }

            if (fixedCommand.StartsWith("2"))
            {
                fixedCommand = "emote";
                command = command.Replace("2", "emote in " + ircBot.Settings.channels[0].name);
                endCommand = fixedCommand.Length;
            }

            foreach (iCommand c in Commands)
            {
                c.handle(command, endCommand, fixedCommand);
            }

            foreach (iGame g in Games)
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
                    Console.WriteLine("\n" + fixedCommand);
                    break;
            }
        }

        public void UpdateUserName(string origUser, string newUser)
        {
            if (m_users.HasUser(origUser))
            {
                User tempUser = m_users[origUser];
                m_users.Remove(origUser);
                m_users.Add(newUser, tempUser);
                Console.WriteLine("Updated username from " + origUser + " to " + newUser);
            }
            else if (newUser != ircBot.Settings.nick)
            {
                m_users.Add(newUser);
            }
        }

        public void ParseUsers(string usersInput)
        {
            Console.WriteLine("Users Input " + usersInput);
            var users = usersInput.Substring(usersInput.LastIndexOf(":") + 1).Split(" ".ToCharArray());

            foreach (var user in users)
            {
                if (user == "") continue;

                if (user != ircBot.Settings.nick && !m_users.HasUser(user))
                {
                    if (user.StartsWith("@"))
                    {
                        m_users.Add(user.Substring(1));
                        Console.WriteLine("Found user " + user.Substring(1));
                    }
                    else
                    {
                        m_users.Add(user);
                        Console.WriteLine("Found user " + user);
                    }
                }
            }

            m_users.PrintUsers();
        }

        public void AddUser(string userName)
        {
            if (!m_users.HasUser(userName))
                m_users.Add(userName);
        }

        public void DirectRoShamBo(string choice)
        {
            RockPaperScissors rps = (RockPaperScissors)Games[(int)GamesList.RockPaperScissors];

            switch (choice)
            {
                case "rock":
		            rps.RPSValue(m_addresser, (int)RoShamBo.Rock);
                    break;
                case "paper":
                    rps.RPSValue(m_addresser, (int)RoShamBo.Paper);
                    break;
                case "scissors":
                    rps.RPSValue(m_addresser, (int)RoShamBo.Scissors);
                    break;
                default:
                    Say("/me whispers something to " + m_addresser + ".");
                    Say("Valid options are either rock, paper, or scissors.", m_addresser);
                    break;
            }

            HandleMessage(":" + ircBot.Settings.command_start + "rps", m_fromChannel, m_addresser);
        }

        public void LastMessage(string user, string inputLine, string fromChannel)
        {
            if (m_users.HasUser(user))
            {
                string message = inputLine.Substring(inputLine.LastIndexOf(fromChannel + " :") + fromChannel.Length + 2);
                m_users.AddUserLastMessage(user, message);
            }
        }

        public string GetAddresser()
        {
            return this.m_addresser;
        }

        public IrcBot GetIrcBot()
        {
            return this.ircBot;
        }

        public Users GetUsers()
        {
            return this.m_users;
        }

        public string GetFromChannel()
        {
            return this.m_fromChannel;
        }

        public string[] GetQuotes()
        {
            return this.ircBot.Settings.quotes;
        }
    }
}
