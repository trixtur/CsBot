using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;

namespace CsBot
{
    class CommandHandler
    {
        private static string NICK = "Be|\\|der";
        private static int DICE = 6;
        public static StreamWriter writer;
        public static StreamReader reader;
        private static string m_addresser = "";
        private static users m_users;
        private const string COMMAND_START = "~";
        private static string CHANNEL = "#pyrous";
        private static string CHANNEL2 = "#CsBot";
        private static string m_fromChannel = CHANNEL;
        private static bool FarkleInSession = false;
        private static Dictionary<int, string> FarkleMembers = new Dictionary<int, string>();
        private static int NumOfFarkleMembers = 0;
        private static int DiceToThrow = 6;
        private static Dictionary<int, int> dice = new Dictionary<int,int>();
        private static int FarkleTotal;
        private static int TempFarkleTotal;
        private static int FarkleUser = 1;
        private enum RoShamBo { Rock, Paper, Scissors };


        public CommandHandler()
        {
            m_users = new users();
            reader = null;
            writer = null;
        }

        public CommandHandler(StreamWriter w, StreamReader r)
        {
            writer = w;
            reader = r;
            m_users = new users();
        }

        /// <summary>
        /// Make the Bot say something in a specific channel.
        /// </summary>
        /// <param name="s">String to Say.</param>
        /// <param name="c">Channel to talk in</param>
        private static void Say(string s, string c)
        {
            if (s.StartsWith("/me"))
                s = "\x1" + s.Replace("/me", "ACTION") + "\x1";
            if (s.EndsWith("me"))
                s = s.Replace(" me", " " + m_addresser);
            if (s.Contains(" me "))
                s = s.Replace(" me ", " " + m_addresser + " ");
            if (s.Contains(" " + NICK + " "))
                s = s.Replace(" " + NICK + " ", " me ");
            Console.WriteLine("PRIVMSG " + c + " :" + s);
            writer.WriteLine("PRIVMSG " + c + " :" + s);
            writer.Flush();
        }

        /// <summary>
        /// Make the Bot say something in a specific channel.
        /// </summary>
        /// <param name="s">String to Say.</param>
        private static void Say(string s)
        {
            if (s.StartsWith("/me"))
                s = "\x1" + s.Replace("/me", "ACTION") + "\x1";
            if (s.EndsWith("me") || s.EndsWith("me"))
                s = s.Replace(" me", " " + m_addresser);
            if (s.Contains(" me "))
                s = s.Replace(" me ", " " + m_addresser + " ");
            if (s.Contains(" " + NICK + " "))
                s = s.Replace(" " + NICK + " ", " me ");
            Console.WriteLine("PRIVMSG " + m_fromChannel + " :" + s);
            writer.WriteLine("PRIVMSG " + m_fromChannel + " :" + s);
            writer.Flush();
        }

        private static string GetChannel(string input)
        {
            string[] parts = input.Split(' ');
            foreach (string part in parts)
            {
                if (part.StartsWith("#"))
                {
                    return part;
                }
            }
            return null;
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
            if (command.Length == endCommand + 1)
            {
                if (fixedCommand.StartsWith(COMMAND_START + "s"))
                {
                    fixedCommand = fixedCommand.Substring(2);
                    command = COMMAND_START + "s "+fixedCommand;
                    fixedCommand = COMMAND_START + "s";
                    endCommand = 2;
                }
            }
            if (fixedCommand.StartsWith(COMMAND_START + "1"))
            {
                fixedCommand = COMMAND_START + "say";
                command = command.Replace(COMMAND_START + "1", COMMAND_START + "say in " + CHANNEL);
                endCommand = fixedCommand.Length;
            }
            if (fixedCommand.StartsWith(COMMAND_START + "2"))
            {
                fixedCommand = COMMAND_START + "emote";
                command = command.Replace(COMMAND_START + "2", COMMAND_START + "emote in " + CHANNEL);
                endCommand = fixedCommand.Length;
            }
            switch (fixedCommand)
            {
                case COMMAND_START + "insult":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": Who do you want " + NICK + " to insult?");
                    }
                    else
                    {
                        string toInsult = command.Substring(endCommand + 2).Trim();
                        if (!m_users.hasUser(toInsult))
                        {
                            Say(m_addresser + ": That person doesn't exist.");
                        }
                        else
                        {
                            Console.WriteLine(m_addresser + " insulted " + toInsult + ".");
                            //Say(m_addresser + ": Ok");
                            //Say(toInsult + ": You Suck!");
                            Say("/me thinks " + toInsult + " isn't the sharpest tool in the shed.");
                        }
                    }
                    break;
                case COMMAND_START + "praise":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": Who do you want " + NICK + " to praise?");
                    }
                    else
                    {
                        string toPraise = command.Substring(endCommand + 2).Trim();
                        if (!m_users.hasUser(toPraise))
                        {
                            Say(m_addresser + ": That person doesn't exist.");
                        }
                        else
                        {
                            Console.WriteLine(m_addresser + " praised " + toPraise + ".");
                            Say("/me thinks " + toPraise + " is very smart.");
                        }
                    }
                    break;
                case COMMAND_START + "apb":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": Who do you want " + NICK + " to find?");
                    }
                    else
                    {
                        string toFind = command.Substring(endCommand + 2).Trim();
                        if (!m_users.hasUser(toFind))
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
                case COMMAND_START + "caffeine":
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
                            Say("/me walks over to " + m_addresser + " and gives them " + shots.ToString() + " shots of caffeine straight into the blood stream.");
                        }
                    }
                    break;
                case COMMAND_START + "say":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": What did you want " + NICK + " to say?");
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
                case COMMAND_START + "emote":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": What did you want " + NICK + " to emote?");
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
                case COMMAND_START + "roll":
                    int d1, d2, total;
                    Random r = new Random();
                    d1 = r.Next(1, DICE);
                    Thread.Sleep(d1 * 100);
                    d2 = r.Next(1, DICE);
                    total = d1 + d2;
                    Say(m_addresser + " rolled a " + d1.ToString() + " and a " + d2.ToString() + " for a total of " + total.ToString());
                    break;
                case COMMAND_START + "rps":
                    if (command.Length == endCommand + 1 && m_users.RPSValue(m_addresser) == -2)
                    {
                        Say("/me whispers something to " + m_addresser + ".");
                        Say("Would you like to throw rock, paper, or scissors?", m_addresser);
                    }
                    else if (m_users.RPSValue(m_addresser) == -2)
                    {
                        Say("Please just use " + COMMAND_START + "rps as a single command. Thanks!");
                    }
                    string opponent;
                    bool isPlaying = m_users.isOpponentPlayingRPS(m_addresser, out opponent); //TODO: This needs to look for a player otherthan myself. Not the first person in the list.
                    Console.WriteLine("isPlaying: " + isPlaying + " opponent: " + opponent);
                    if (isPlaying && (!opponent.Equals(m_addresser)) && m_users.RPSValue(m_addresser) != -2 && m_users.RPSValue(opponent) != -2)
                    {
                        int opponent_throw = m_users.RPSValue(opponent);
                        int my_throw = m_users.RPSValue(m_addresser);
                        m_users.StopRPS(opponent);
                        m_users.StopRPS(m_addresser);
                        if (opponent_throw == my_throw)
                            Say("The Rock, Paper, Scissors game between " + opponent + " and " + m_addresser + " ended in a tie.");
                        else if (opponent_throw == (int)RoShamBo.Rock && my_throw == (int)RoShamBo.Scissors)
                            Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                        else if (opponent_throw == (int)RoShamBo.Scissors && my_throw == (int)RoShamBo.Rock)
                            Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                        else if (opponent_throw == (int)RoShamBo.Paper && my_throw == (int)RoShamBo.Scissors)
                            Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                        else if (opponent_throw == (int)RoShamBo.Scissors && my_throw == (int)RoShamBo.Paper)
                            Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                        else if (opponent_throw == (int)RoShamBo.Paper && my_throw == (int)RoShamBo.Rock)
                            Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                        else if (opponent_throw == (int)RoShamBo.Rock && my_throw == (int)RoShamBo.Paper)
                            Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                    }
                    else if (opponent.Equals(string.Empty) && !m_users.isPlayingRPS(m_addresser))
                    {
                        Say(m_addresser + " is looking for an opponent in Rock, Paper, Scissors.");
                    }
                    break;
                case COMMAND_START + "farklehelp":
                    Say("To play farkle you have the following commands:", m_addresser);
                    Say(COMMAND_START + "farkle Rolls at the start of game play", m_addresser);
                    Say("and is used when all rolled dice are scoring.", m_addresser);
                    Say(COMMAND_START + "farkle n #  Re-rolls # of dice, and keeps the highest of the rest.", m_addresser);
                    Say(COMMAND_START + "farkle y Keeps all dice and ends your turn.", m_addresser);
                    Say(COMMAND_START + "farklescore <player> Returns your score or players if provided.", m_addresser);
                    Say(COMMAND_START + "joinfarkle Join a new game of farkle.", m_addresser);
                    Say(COMMAND_START + "farklehelp This help information.", m_addresser);
                    Say(COMMAND_START + "farkelforfeit To stop in the middle of a game.", m_addresser);
                    break;
                case COMMAND_START + "farkleforfeit":
                    if(m_users.isPlayingFarkle(m_addresser))
                    {
                        m_users.SetFarkleFlag(m_addresser, false);
                        m_users.SetFarkleToken(m_addresser, false);
                        Say(m_addresser + " forfeit.");
                        foreach (int member in FarkleMembers.Keys)
                        {
                            if (FarkleMembers[member] == m_addresser)
                            {
                                FarkleMembers.Remove(member);
                                break;
                            }
                        }
                        string name = m_addresser;
                        {
                            while (FarkleUser < FarkleMembers.Count + 5)
                            {
                                FarkleMembers.TryGetValue(FarkleUser, out name);
                                if (name == null)
                                    name = "";
                                if (m_users.hasUser(name))
                                {
                                    Say(name + " it is now your turn.");
                                    break;
                                }
                                FarkleUser++;
                            }
                            if (!m_users.hasUser(name))
                                FarkleUser = 1;
                        }
                    }
                    break;
                case COMMAND_START + "farklescore":
                    if (command.Length == endCommand + 1)
                        Say(m_addresser + " your score is " + m_users.FarkleValue(m_addresser) + ".");
                    else
                    {
                        string name = command.Substring(endCommand + 2).Trim();
                        if (m_users.hasUser(name))
                                Say(name + "'s score is " + m_users.FarkleValue(name) + ".");
                        else
                        {
                            Say(m_addresser + " that user does not exist.");
                            break;
                        }
                    }
                    break;
                case COMMAND_START + "farkle":
                    if (FarkleMembers.Count == 0)
                    {
                        Say(m_addresser + " no game in session.", m_addresser);
                        break;
                    }

                    if (m_users.FarkleValue(m_addresser) >= 5000 || FarkleMembers.Count == 1)
                    {
                        DeclareWinner();
                        break;
                    }
                    if (!m_users.SomeoneHasToken() && m_users.isPlayingFarkle())
                        m_users.SetFarkleToken(FarkleMembers[FarkleUser], true);
                    if (command.Length == endCommand + 1)
                    {
                        if (FarkleDiceAllScoring() && m_users.GetFarkleToken(m_addresser) && TempFarkleTotal > 0)
                        {
                            FarkleTotal += TempFarkleTotal;
                            TempFarkleTotal = 0;
                            DiceToThrow = 0;
                        }
                        else if (!m_users.GetFarkleToken(m_addresser))
                        {
                            Say(m_addresser + " that is someone else's dice.", m_addresser);
                            break;
                        }
                        if (FarkleMembers.Count < 2)
                        {
                            Say("You need atleast 2 people to play. Use " + COMMAND_START + "joinfarkle to join the game.");
                            break;
                        }
                        if (DiceToThrow > 0 && m_users.GetFarkleToken(m_addresser) && dice.Count != 0)
                        {
                            Say(m_addresser + " you have already rolled once, instead answer the question.", m_addresser);
                            Say(m_addresser + " use " + COMMAND_START + "farkle n # to rethrow dice.", m_addresser);
                            break;
                        }
                        if (DiceToThrow == 0 && m_users.GetFarkleToken(m_addresser))
                        {
                            Say(m_addresser + " congratulations on all dice getting a scoring number.");
                            dice.Clear();
                            DiceToThrow = 6;
                        }
                        FarkleRoll();
                        
                    }
                    else if(command.Substring(endCommand + 2).Trim().ToLower() == "y")
                    {
                        if (!FarkleInSession)
                        {
                            Say(m_addresser + " no game in session.", m_addresser);
                            break;
                        }

                        if (!m_users.GetFarkleToken(FarkleMembers[FarkleUser]) || dice.Count == 0)
                        {
                            Say(m_addresser + " you cannot keep something you do not have.", m_addresser);
                            break;
                        }
                        FarkleTotal += TempFarkleTotal;
                        m_users.FarkleValue(m_addresser, FarkleTotal);
                        m_users.SetFarkleToken(m_addresser, false);
                        dice.Clear();
                        Say(m_addresser + " you kept " + FarkleTotal + " for a total of " + m_users.FarkleValue(m_addresser) + ".");
                        FarkleTotal = 0;
                        TempFarkleTotal = 0;
                        DiceToThrow = 6;
                        FarkleUser++;
                        string name = "";
                        bool found = false;
                        foreach (string user in FarkleMembers.Values)
                        { //Get the next user in the list
                            if (found) 
                            {
                                name = user;
                                break;
                            } else if (user.Equals(m_addresser))
                            {
                                    found = true;
                            }
                        }
                        if (name == "") { //If we were at the end of the list.
                            foreach(string user in FarkleMembers.Values) {
                                name = user;
                                break;
                            }
                        }

                        Say(name + ", it is now your turn.");
                        m_users.SetFarkleToken(name, true);
                        return;
                    }
                    else if(command.Substring(endCommand + 2).Trim().ToLower().Contains("n") && command.Trim().ToLower().Contains(COMMAND_START + "farkle n "))
                    {
                        if (!FarkleInSession)
                        {
                            Say(m_addresser + " no game in session.", m_addresser);
                            break;
                        }

                        if (m_users.isPlayingFarkle(m_addresser) == false || !m_users.GetFarkleToken(m_addresser))
                        {
                            Say(m_addresser + " please don't try and throw dice on someone elses turn, wait until the end.", m_addresser);
                            return;
                        }
                        if (dice.Count <= 0)
                        {
                            Say(m_addresser + " you cannot remove dice you have not thrown.", m_addresser);
                            return;
                        }
                        string infoOutput = null;
                        int highestScoring = 0;
                        int diceToRemove = 1;
                        int tempPartDice = 0;
                        bool possible = true;
                        int whileItterations = 1;
                        command = command.Replace(":" + COMMAND_START + "farkle n ", "");
                        if (FarkleDiceAllScoring())
                        {
                            //FarkleTotal += TempFarkleTotal;
                            Say(m_addresser + " all dice scoring, so added " + FarkleTotal + " to your score " + COMMAND_START + "farkle to continue.");
                            DiceToThrow = 0;
                            break;
                        }
                        foreach (int die in dice.Keys)
                        {
                            int number;
                            if (int.TryParse(command, out number) && FarkleValueCheck(die, DiceToThrow - number) > 0)
                            {
                                possible = true;
                                break;
                            }
                            else
                                possible = false;


                        }
                        if (!possible)
                        {
                            Say(m_addresser + " you cannot re-throw that many.", m_addresser);
                            break;
                        }
                        while (diceToRemove > 0 && whileItterations < 10)
                        {
                            diceToRemove = DiceToThrow - int.Parse(command);
                            foreach (int die in dice.Keys)
                            {
                                if (highestScoring < FarkleValueCheck(die, diceToRemove))
                                {
                                    highestScoring = FarkleValueCheck(die, diceToRemove);
                                    tempPartDice = die;
                                }
                            }
                            if (dice.ContainsKey(tempPartDice))
                            {
                                if (diceToRemove <= 0)
                                {
                                    Say(m_addresser + " you cannot remove that many dice.", m_addresser);
                                    return;
                                }
                                else if (dice[tempPartDice] == diceToRemove)
                                {
                                    if (dice[tempPartDice] > 3 && dice[tempPartDice] < 6)
                                    {
                                        diceToRemove -= 3;
                                        for (int i = 0; i < 3; i++)
                                            infoOutput += tempPartDice + ", ";
                                        FarkleTotal += FarkleValueCheck(tempPartDice, dice[tempPartDice]);
                                        dice[tempPartDice] -= 3;
                                    }
                                    else
                                    {
                                        diceToRemove -= dice[tempPartDice];
                                        for (int i = 0; i < dice[tempPartDice]; i++)
                                            infoOutput += tempPartDice + ", ";
                                        FarkleTotal += FarkleValueCheck(tempPartDice, dice[tempPartDice]);
                                        dice.Remove(tempPartDice);
                                    }
                                }
                                else if (dice[tempPartDice] > diceToRemove)
                                {
                                    if (dice[tempPartDice] > 3 && dice[tempPartDice] < 6)
                                    {
                                        for (int i = 0; i < 3; i++)
                                            infoOutput += tempPartDice + ", ";
                                        FarkleTotal += FarkleValueCheck(tempPartDice, diceToRemove);
                                        dice[tempPartDice] -= 3;
                                        diceToRemove -= 3;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < diceToRemove; i++)
                                            infoOutput += tempPartDice + ", ";
                                        FarkleTotal += FarkleValueCheck(tempPartDice, diceToRemove);
                                        dice[tempPartDice] -= diceToRemove;
                                        diceToRemove = 0;
                                    }
                                }
                                else
                                {
                                    diceToRemove -= dice[tempPartDice];
                                    for (int i = 0; i < dice[tempPartDice]; i++)
                                        infoOutput += tempPartDice + ", ";
                                    FarkleTotal += FarkleValueCheck(tempPartDice, dice[tempPartDice]);
                                    dice.Remove(tempPartDice);
                                }
                            }
                            highestScoring = 0;
                            whileItterations++;
                        }
                        infoOutput = infoOutput.Remove(infoOutput.Length - 2);
                        Say(m_addresser + " kept " + infoOutput + ".");
                        TempFarkleTotal = 0;
                        DiceToThrow = 0;
                        foreach (int die in dice.Keys)
                        {
                            DiceToThrow += dice[die];
                        }
                        if (DiceToThrow <= int.Parse(command))
                            DiceToThrow = int.Parse(command);
                        dice.Clear();
                        FarkleRoll();
                    }
                    else
                    {
                        if (!m_users.GetFarkleToken(m_addresser) || TempFarkleTotal != 0 || FarkleTotal != 0)
                        {
                            Say(m_addresser + " either you do not have the token or you need to type " + COMMAND_START + "farkle y/" + COMMAND_START + "farkle n #.", m_addresser);
                            break;
                        }
                        FarkleTotal += TempFarkleTotal;
                        m_users.FarkleValue(m_addresser, FarkleTotal);
                        m_users.SetFarkleToken(m_addresser, false);
                        dice.Clear();
                        Say(m_addresser + " you kept " + FarkleTotal + " for a total of " + m_users.FarkleValue(m_addresser) + ".");
                        FarkleTotal = 0;
                        TempFarkleTotal = 0;
                        DiceToThrow = 6;
                        FarkleUser++;
                        string name = "";
                        bool found = false;
                        foreach (string user in FarkleMembers.Values)
                        { //Get the next user in the list
                            if (found) 
                            {
                                name = user;
                                break;
                            } else if (user.Equals(m_addresser))
                            {
                                    found = true;
                            }
                        }
                        if (name == "") { //If we were at the end of the list.
                            foreach(string user in FarkleMembers.Values) {
                                name = user;
                                break;
                            }
                        }
                        m_users.SetFarkleToken(name, true);
                        Say(name + ", it is now your turn.");
                    }

                    if(!FarkleInSession)
                        FarkleInSession = true;
                    break;
                case COMMAND_START + "joinfarkle":
                    if (!FarkleInSession)
                    {
                        m_users.ClearFarkleScores();
                        if (m_users.isPlayingFarkle(m_addresser))
                        {
                            Say(m_addresser + " you are already playing farkle.", m_addresser);
                            break;
                        }
                        if (!m_users.isPlayingFarkle())
                        {
                            Say(m_addresser + " has started a game of farkle, to join type " + COMMAND_START + "joinfarkle");
                            m_users.SetFarkleToken(m_addresser, true);
                        }
                    }
                    else
                    {
                        Say(m_addresser + " you cannot join when a game is already being played.", m_addresser);
                        break;
                    }
                    Say(m_addresser + " welcome to this game of farkle.");
                    FarkleMembers.Add(++NumOfFarkleMembers, m_addresser);
                    m_users.SetFarkleFlag(m_addresser, true);
                    break;
                case COMMAND_START + "s":
                    if (command.Length == endCommand + 1)
                    {
                        Say(m_addresser + ": What did you want " + NICK + " to replace?");
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
                case COMMAND_START + "getfeeds":
                    StreamReader reader;
                    WebRequest rss;
                    if (command.Length == endCommand + 1)
                    {
                        rss = WebRequest.Create("http://rss.news.yahoo.com/rss/topstories#");
                    }
                    else
                    {
                        rss = WebRequest.Create(command.Substring(endCommand + 2).Trim().ToLower());
                    }
                    Stream ansstream;
                    WebResponse ans;
                    string stringans;
                    int timer = 0;
                    string output = null;
                    string[] linedoutput = null;
                    ans = rss.GetResponse();
                    ansstream = ans.GetResponseStream();
                    reader = new StreamReader(ansstream);
                    while (!reader.EndOfStream)
                    {
                        stringans = reader.ReadToEnd();
                        while (stringans.Contains("<item>"))
                        {
                            output += stringans.Substring(stringans.IndexOf("<item>") + 6, stringans.IndexOf("</item>") - 7 - stringans.IndexOf("<item>"));
                            stringans = stringans.Remove(stringans.IndexOf("<item>"), stringans.IndexOf("</item>") - stringans.IndexOf("<item>") + 7);
                        }
                        while (output.Contains("<link>"))
                        {
                            output = output.Remove(output.IndexOf("<link>"), output.IndexOf("</pubDate>") - output.IndexOf("<link>") + 10);
                        }
                        while (output.Contains("<media"))
                        {
                           output = output.Remove(output.IndexOf("<media"), output.IndexOf("</media:credit>") - output.IndexOf("<media") + 15);
                        }
                        while (output.Contains("/a>"))
                        {
                            while (output.IndexOf("/a>") < output.IndexOf("<description>"))
                                output = output.Remove(output.IndexOf("/a>"), 3);
                            if (output.IndexOf("<description>") < 0)
                                output = output.Remove(output.IndexOf("/a>"), 3);
                            else
                                output = output.Remove(output.IndexOf("<description>"), output.IndexOf("/a>") - output.IndexOf("<description>") + 3);
                        }
                        while (output.Contains("<") || output.Contains(">"))
                        {
                            while(output.IndexOf('>') < output.IndexOf('<'))
                                output = output.Remove(output.IndexOf('>'), 1);
                            output = output.Remove(output.IndexOf('<'), output.IndexOf('>') - output.IndexOf('<') + 1);
                        }
                        while (output.Contains("&#60;"))
                        {
                            output = output.Remove(output.IndexOf("&#60;"), output.IndexOf("\"/") - output.IndexOf("&#60;") + 2);
                        }
                        while (output.Contains("&#39;"))
                        {
                            output = output.Replace("&#39;", "'");
                        }
                        while (output.Contains("&#039;"))
                        {
                            output = output.Replace("&#039;", "'");
                        }
                        output.Trim();
                        linedoutput = output.Split('\n');
                        for (int i = 0; i < linedoutput.Length; i++)
                        {
                            Say(linedoutput[i].Trim(), m_addresser);
                            timer++;
                            if (timer > 4)
                            {
                                Thread.Sleep(3000);
                                timer = 0;
                            }
                        }
                    }
                    reader.Close();
                    break;
                default:
                    Console.WriteLine("\n" + fixedCommand);
                    break;
            }
        }

        private static bool FarkleDiceAllScoring()
        {
            foreach (int die in dice.Keys)
            {
                if (FarkleValueCheck(die, dice[die]) == 0)
                    return false;
                else if (dice.Count == 3 && !dice.ContainsValue(1) && !dice.ContainsValue(3))
                    return true;
                else if (dice.Count == 6)
                    return true;
            }
            return true;
        }

        private static void FarkleRoll()
        {
            string infoOutput = null;
            int temp;
            Random dieRandom = new Random();
            if (m_users.isPlayingFarkle(m_addresser) == false || !m_users.GetFarkleToken(m_addresser))
            {
                Say(m_addresser + " please don't try and throw dice on someone elses turn, wait until the end.", m_addresser);
                return;
            }
            for (int i = 0; i < DiceToThrow; i++)
            {
                int tempValue;
                temp = (dieRandom.Next(1, 100)%6) + 1;
                if (dice.ContainsKey(temp))
                {
                    dice.TryGetValue(temp, out tempValue);
                    dice[temp] = ++tempValue;
                }
                else
                {
                    dice.Add(temp, 1);
                }
                Thread.Sleep(temp * 50);
            }
            foreach (int outputdice in dice.Keys)
            {
                if (dice[outputdice] > 1)
                {
                    for (int i = 0; i < dice[outputdice]; i++ )
                        infoOutput += outputdice + ", ";
                }
                else
                    infoOutput += outputdice + ", ";
            }
            infoOutput = infoOutput.Remove(infoOutput.Length - 2);
            if (dice.Count == 6)
            {
                FarkleTotal += 1000;
                DiceToThrow = 0;
            }
            else if (FarkleIsThreePairs(dice))
            {
                FarkleTotal += 1000;
                DiceToThrow = 0;
            }
            else
            {
                if (FarkleValueCheck(dice) == 0)
                {
                    m_users.SetFarkleToken(m_addresser, false);
                    dice.Clear();
                    FarkleTotal = 0;
                    TempFarkleTotal = 0;
                    DiceToThrow = DICE;
                    Say(m_addresser + " you rolled " + infoOutput + " for a total of " + (FarkleTotal + TempFarkleTotal) + ".");
                    Say("You bust.");
                    FarkleUser++;
                    if (!FarkleMembers.ContainsKey(FarkleUser))
                    {
                        FarkleUser++;
                        if(!FarkleMembers.ContainsKey(FarkleUser))
                            FarkleUser = 1;
                    }
                    m_users.SetFarkleFlag(FarkleMembers[FarkleUser], true);
                    Say(FarkleMembers[FarkleUser] + ", it is now your turn.");
                    return;
                }
                else
                {
                    TempFarkleTotal = FarkleValueCheck(dice);
                }
            }
            Say(m_addresser + " you rolled " + infoOutput + " for a total of " + (FarkleTotal + TempFarkleTotal) + ".");
        }

        public static void ParseUsers(string usersInput)
        {
            string[] users = usersInput.Substring(usersInput.LastIndexOf(NICK)).Split(" ".ToCharArray());
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] != NICK && !m_users.hasUser(users[i]))
                {
                    if (users[i].StartsWith("@"))
                        m_users.addUser(users[i].Substring(1));
                    else
                        m_users.addUser(users[i]);
                }
            }
        }

        private static bool FarkleIsThreePairs(Dictionary<int, int> dice)
        {
            if (dice.Count == 3)
            {
                foreach (int die in dice.Keys)
                {
                    if (dice[die] != 2)
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddUser(string userName)
        {
            if (!m_users.hasUser(userName))
            {
                m_users.addUser(userName);
            }
        }

        private static int FarkleValueCheck(int dieKey, int numToKeep)
        {
            int returnValue = 0;
            int numDiceToCheck = 0;
            if (dice[dieKey] > numToKeep)
                numDiceToCheck = numToKeep;
            else
                numDiceToCheck = dice[dieKey];
            if (dieKey == 1)
            {
                switch (numDiceToCheck)
                {
                    case 1:
                        returnValue += 100;
                        break;
                    case 2:
                        returnValue += 200;
                        break;
                    case 3:
                        returnValue += 1000;
                        break;
                    case 4:
                        returnValue += 1100;
                        break;
                    case 5:
                        returnValue += 1200;
                        break;
                    case 6:
                        returnValue += 2000;
                        break;
                }
            }
            else if (dieKey == 5)
            {
                switch (numDiceToCheck)
                {
                    case 1:
                        returnValue += 50;
                        break;
                    case 2:
                        returnValue += 100;
                        break;
                    case 3:
                        returnValue += 500;
                        break;
                    case 4:
                        returnValue += 550;
                        break;
                    case 5:
                        returnValue += 600;
                        break;
                    case 6:
                        returnValue += 1000;
                        break;
                }
            }
            else
            {
                switch (numDiceToCheck)
                {
                    case 3:
                    case 4:
                    case 5:
                        returnValue += dieKey * 100;
                        break;
                }
            }
            return returnValue;
        }

        private static int FarkleValueCheck(Dictionary<int, int> dice)
        {
            int returnValue = 0;
            foreach (int die in dice.Keys)
            {
                if (die == 1)
                {
                    switch (dice[die])
                    {
                        case 1:
                            returnValue += 100;
                            break;
                        case 2:
                            returnValue += 200;
                            break;
                        case 3:
                            returnValue += 1000;
                            break;
                        case 4:
                            returnValue += 1100;
                            break;
                        case 5:
                            returnValue += 1200;
                            break;
                        case 6:
                            returnValue += 2000;
                            break;
                    }
                }
                else if (die == 5)
                {
                    switch (dice[die])
                    {
                        case 1:
                            returnValue += 50;
                            break;
                        case 2:
                            returnValue += 100;
                            break;
                        case 3:
                            returnValue += 500;
                            break;
                        case 4:
                            returnValue += 550;
                            break;
                        case 5:
                            returnValue += 600;
                            break;
                        case 6:
                            returnValue += 1000;
                            break;
                    }
                }
                else
                {
                    switch (dice[die])
                    {
                        case 3:
                        case 4:
                        case 5:
                            returnValue += die * 100;
                            break;
                    }
                }
            }
            return returnValue;
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
            if (m_fromChannel != CHANNEL && m_fromChannel != CHANNEL2) {
                m_fromChannel = CHANNEL;
            }

            HandleMessage(":" + COMMAND_START + "rps", m_fromChannel, m_addresser);
        }

        public void LastMessage(string user, string inputLine)
        {
            if (m_users.hasUser(user))
            {
                string message = inputLine.Substring(inputLine.LastIndexOf(CHANNEL + " :") + CHANNEL.Length + 2);
                m_users.addUserLastMessage(user, message);
            }
        }

        private void DeclareWinner() {
            bool tie = false;
            string winner = m_addresser;
            foreach(string user in FarkleMembers.Values) {
                if(m_users.FarkleValue(user) >= 5000 && m_users.FarkleValue(user) > m_users.FarkleValue(winner)) {
                    winner = user;
                } else if (m_users.FarkleValue(user) >= 5000 && m_users.FarkleValue(user) == m_users.FarkleValue(winner) && !user.Equals(winner)) {
                    winner = user;
                    tie = true;
                }
            }
            if (tie) {
                Say("This match ended in a tie between " + m_addresser + " and " + winner + " at " 
                        + m_users.FarkleValue(winner) + " points!!!");
            } else {
                Say(winner + " won with " + m_users.FarkleValue(winner) + " points!!!");
            }
            foreach (int member in FarkleMembers.Keys)
            {
                m_users.SetFarkleFlag(FarkleMembers[member], false);
                m_users.SetFarkleToken(FarkleMembers[member], false);
            }
            NumOfFarkleMembers = 0;
            dice.Clear();
            FarkleMembers.Clear();
            FarkleInSession = false;
        }
    }
}
