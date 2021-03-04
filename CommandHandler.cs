using System;
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
        int DICE = 6;
        internal string m_addresser = "";
        internal readonly Users m_users;
        Farkle farkle;
        RssFeedCommand rssFeed;

        public CommandHandler(IrcBot ircBot)
        {
            this.ircBot = ircBot;
            m_users = new Users();
            rssFeed = new RssFeedCommand(this);
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

        string GetChannel(string input)
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

            switch (fixedCommand)
            {
                case "insult":
                    if (true) {
                        new Insult(this).handle(command, endCommand);
                    } else {
                        if (command.Length == endCommand + 1)
                        {
                            Say(m_addresser + ": Who do you want " + ircBot.Settings.nick + " to insult?");
                        }
                        else
                        {
                            string toInsult = command.Substring(endCommand + 2).Trim();
                            if (!m_users.HasUser(toInsult))
                            {
                                Say(m_addresser + ": That person doesn't exist.");
                            }
                            else
                            {
                                Console.WriteLine(m_addresser + " insulted " + toInsult + ".");
                                if (ircBot.Settings.insults != null && ircBot.Settings.insults.Length > 0)
                                {
                                    Say("/me " + string.Format(ircBot.Settings.insults[random.Next(0, 10000) % ircBot.Settings.insults.Length], toInsult));
                                }
                                else
                                {
                                    Say("/me thinks " + toInsult + " is screwier than his Aunt Rita, and she's a screw.");
                                }
                            }
                        }
                    }
                    break;
                case "quote":
                    if (ircBot.Settings.quotes != null && ircBot.Settings.quotes.Length > 0)
                    {
                        Say(ircBot.Settings.quotes[random.Next(0, 10000) % ircBot.Settings.quotes.Length]);
                    }
                    else
                    {
                        Say("Hey, the blues. The tragic sound of other people's suffering. Thant's kind of a pick-me-up.");
                    }
                    break;
                case "praise":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": Who do you want " + ircBot.Settings.nick + " to praise?");
                    }
                    else
                    {
                        string toPraise = command.Substring(endCommand + 2).Trim();
                        if (!m_users.HasUser(toPraise))
                        {
                            Say(m_addresser + ": That person doesn't exist.");
                        }
                        else
                        {
                            Console.WriteLine(m_addresser + " praised " + toPraise + ".");
                            if (ircBot.Settings.praises != null && ircBot.Settings.praises.Length > 0)
                            {
                                Say("/me " + string.Format(ircBot.Settings.praises[random.Next(0, 10000) % ircBot.Settings.praises.Length], toPraise));
                            }
                            else
                            {
                                Say("/me thinks " + toPraise + " is very smart.");
                            }
                        }
                    }
                    break;
                case "apb":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": Who do you want " + ircBot.Settings.nick + " to find?");
                    }
                    else
                    {
                        string toFind = command.Substring(endCommand + 2).Trim();
                        if (!m_users.HasUser(toFind))
                        {
                            Say(m_addresser + ": That person doesn't exist.");
                        }
                        else
                        {
                            Console.WriteLine(m_addresser + " put out apb for " + toFind);
                            Say("/me sends out the blood hounds to find " + toFind + ".");
                        }
                    }
                    break;
                case "caffeine":
                    if (command.Length == endCommand + 1)
                    {
                        Say("/me walks over to " + m_addresser + " and gives them a shot of caffeine straight into the blood stream.");
                    }
                    else
                    {
                        int shots;
                        if (!int.TryParse(command.Substring(endCommand + 2).Trim(), out shots))
                        {
                            Say(m_addresser + ": I didn't understand, how many shots of caffeine did you want?");
                        }
                        else if (shots == 1)
                        {
                            Say("/me walks over to " + m_addresser + " and gives them a shot of caffeine straight into the blood stream.");
                        }
                        else
                        {
                            Say("/me walks over to " + m_addresser + " and gives them " + shots + " shots of caffeine straight into the blood stream.");
                        }
                    }
                    break;
                case "say":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": What did you want " + ircBot.Settings.nick + " to say?");
                    }
                    else
                    {
                        string toSay = command.Substring(endCommand + 2).Trim();
                        if (toSay.StartsWith("in"))
                        {
                            string channel = GetChannel(toSay);
                            if (channel != null)
                            {
                                string toSayIn = toSay.Substring(toSay.IndexOf(channel) + channel.Length + 1);
                                Say(toSayIn, channel);
                            }
                        }
                        else
                        {
                            Say(toSay, m_fromChannel);
                        }
                    }
                    break;
                case "emote":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": What did you want " + ircBot.Settings.nick + " to emote?");
                    }
                    else
                    {
                        string toEmote = command.Substring(endCommand + 2).Trim();
                        if (toEmote.StartsWith("in"))
                        {
                            string channel = GetChannel(toEmote);
                            if (channel != null)
                            {
                                string toEmoteIn = toEmote.Substring(toEmote.IndexOf(channel) + channel.Length + 1);
                                Say("/me " + toEmoteIn, channel);
                            }
                        }
                        else
                        {
                            Say("/me " + toEmote);
                        }
                    }
                    break;
                case "roll":
                    int d1, d2, total;
                    d1 = random.Next(1, DICE + 1);
                    d2 = random.Next(1, DICE + 1);
                    total = d1 + d2;
                    Say(m_addresser + " rolled a " + d1 + " and a " + d2 + " for a total of " + total);
                    break;
                case "rps":
                    new RockPaperScissors(this).Play(this, command, endCommand);
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
                case "s":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": What did you want " + ircBot.Settings.nick + " to replace?");
                    }
                    else if (command.IndexOf("/") == -1)
                    {
                        Say(m_addresser + ": Usage is ~s/<wrong phrase>/<corrected phrase>/");
                    }
                    else
                    {
                        string toReplace = command.Substring(command.IndexOf("/") + 1, command.Substring(command.IndexOf("/") + 1).IndexOf("/"));
                        string withString = command.Substring(command.IndexOf("/", command.IndexOf(toReplace))).Replace("/", "");
                        string lastSaid = m_users.getUserMessage(m_addresser);
                        lastSaid = lastSaid.Replace(toReplace, withString);
                        Say(m_addresser + " meant: " + lastSaid);
                    }
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
            switch (choice)
            {
                case "rock":
                    m_users.RPSValue(m_addresser, (int)RoShamBo.Rock);
                    break;
                case "paper":
                    m_users.RPSValue(m_addresser, (int)RoShamBo.Paper);
                    break;
                case "scissors":
                    m_users.RPSValue(m_addresser, (int)RoShamBo.Scissors);
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
    }
}
