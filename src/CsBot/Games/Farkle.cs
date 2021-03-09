using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsBot.Games
{
    class Farkle
    {
        static readonly Random random = new Random();

        CommandHandler handler;
        bool FarkleInSession = false;
        readonly Dictionary<int, string> FarkleMembers = new Dictionary<int, string>();
        int NumOfFarkleMembers = 0;
        int DICE = 6;
        int DiceToThrow = 6;
        readonly Dictionary<int, int> dice = new Dictionary<int, int>();
        int FarkleTotal;
        int TempFarkleTotal;

        Users m_users => handler.m_users;
        string m_addresser => handler.m_addresser;
        IrcBot ircBot => handler.ircBot;


        public Farkle(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void FarkleCommand(string command, int endCommand)
        {
            if (FarkleMembers.Count == 0)
            {
                handler.Say(m_addresser + " no game in session.", m_addresser);
                return;
            }

            if (m_users.FarkleValue(m_addresser) >= 5000 || FarkleMembers.Count == 1)
            {
                DeclareWinner();
                return;
            }

            /*if (!m_users.SomeoneHasToken() && m_users.isPlayingFarkle()) This shouldn't be needed
                m_users.SetFarkleToken(FarkleMembers[FarkleUser], true);*/

            if (!m_users.GetFarkleToken(m_addresser))
            {
                handler.Say(m_addresser + " that is someone else's dice.", m_addresser);
                return;
            }

            if (FarkleMembers.Count < 2)
            {
                handler.Say("You need atleast 2 people to play. Use " + ircBot.Settings.command_start + "joinfarkle to join the game.");
                return;
            }
            // Done with preliminary checks

            if (command.Length == endCommand + 1)
            {
                if (FarkleDiceAllScoring() && m_users.GetFarkleToken(m_addresser) && TempFarkleTotal > 0)
                {
                    FarkleTotal += TempFarkleTotal;
                    TempFarkleTotal = 0;
                    DiceToThrow = 0;
                }
                if (DiceToThrow > 0 && m_users.GetFarkleToken(m_addresser) && dice.Count != 0)
                {
                    handler.Say(m_addresser + " you have already rolled once, instead answer the question.", m_addresser);
                    handler.Say(m_addresser + " use " + ircBot.Settings.command_start + "farkle n # to rethrow dice.", m_addresser);
                    return;
                }
                if (DiceToThrow == 0 && m_users.GetFarkleToken(m_addresser))
                {
                    handler.Say(m_addresser + " congratulations on all dice getting a scoring number.");
                    dice.Clear();
                    DiceToThrow = 6;
                }
                FarkleRoll();

            }
            else if (command.Substring(endCommand + 2).Trim().ToLower() == "y")
            {
                if (!FarkleInSession)
                {
                    handler.Say(m_addresser + " no game in session.", m_addresser);
                    return;
                }

                if (!m_users.GetFarkleToken(m_addresser) || dice.Count == 0)
                {
                    handler.Say(m_addresser + " you cannot keep something you do not have.", m_addresser);
                    return;
                }
                FarkleTotal += TempFarkleTotal;
                m_users.FarkleValue(m_addresser, FarkleTotal);
                m_users.SetFarkleToken(m_addresser, false);
                dice.Clear();
                handler.Say(m_addresser + " you kept " + FarkleTotal + " for a total of " + m_users.FarkleValue(m_addresser) + ".");
                FarkleTotal = 0;
                TempFarkleTotal = 0;
                DiceToThrow = 6;
                string name = "";
                bool found = false;
                foreach (string user in FarkleMembers.Values)
                { //Get the next user in the list
                    if (found)
                    {
                        name = user;
                        return;
                    }
                    else if (user.Equals(m_addresser))
                    {
                        found = true;
                    }
                }
                if (name == "")
                { //If we were at the end of the list.
                    foreach (string user in FarkleMembers.Values)
                    {
                        name = user;
                        return;
                    }
                }

                handler.Say(name + ", it is now your turn.");
                m_users.SetFarkleToken(name, true);
                return;
            }
            else if (command.Substring(endCommand + 2).Trim().ToLower().Contains("n") && command.Trim().ToLower().Contains("farkle n "))
            {
                if (!FarkleInSession)
                {
                    handler.Say(m_addresser + " no game in session.", m_addresser);
                    return;
                }

                if (m_users.IsPlayingFarkle(m_addresser) == false || !m_users.GetFarkleToken(m_addresser))
                {
                    handler.Say(m_addresser + " please don't try and throw dice on someone elses turn, wait until the end.", m_addresser);
                    return;
                }
                if (dice.Count <= 0)
                {
                    handler.Say(m_addresser + " you cannot remove dice you have not thrown.", m_addresser);
                    return;
                }
                string infoOutput = null;
                int highestScoring = 0;
                int diceToRemove = 1;
                int tempPartDice = 0;
                bool possible = true;
                int whileItterations = 1;
                command = command.Replace(":" + ircBot.Settings.command_start + "farkle n ", "");
                if (FarkleDiceAllScoring())
                {
                    //FarkleTotal += TempFarkleTotal;
                    handler.Say(m_addresser + " all dice scoring, so added " + FarkleTotal + " to your score " + ircBot.Settings.command_start + "farkle to continue.");
                    DiceToThrow = 0;
                    return;
                }
                foreach (int die in dice.Keys)
                {
                    int number;
                    if (int.TryParse(command, out number) && FarkleValueCheck(die, DiceToThrow - number) > 0)
                    {
                        possible = true;
                        return;
                    }
                    else
                        possible = false;


                }
                if (!possible)
                {
                    handler.Say(m_addresser + " you cannot re-throw that many.", m_addresser);
                    return;
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
                            handler.Say(m_addresser + " you cannot remove that many dice.", m_addresser);
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
                            /*diceToRemove -= dice[tempPartDice]; Old way that gave weird output, but ultimately worked
                            for (int i = 0; i < dice[tempPartDice]; i++)
                                infoOutput += tempPartDice + ", ";
                            FarkleTotal += FarkleValueCheck(tempPartDice, dice[tempPartDice]);
                            dice.Remove(tempPartDice);*/
                        }
                    }
                    highestScoring = 0;
                    whileItterations++;
                }
                infoOutput = infoOutput.Remove(infoOutput.Length - 2);
                handler.Say(m_addresser + " kept " + infoOutput + ".");
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
                    handler.Say(m_addresser + " use " + ircBot.Settings.command_start + "farkle y/" + ircBot.Settings.command_start + "farkle n #.", m_addresser);
                    return;
                }
                FarkleTotal += TempFarkleTotal;
                m_users.FarkleValue(m_addresser, FarkleTotal);
                m_users.SetFarkleToken(m_addresser, false);
                dice.Clear();
                handler.Say(m_addresser + " you kept " + FarkleTotal + " for a total of " + m_users.FarkleValue(m_addresser) + ".");
                FarkleTotal = 0;
                TempFarkleTotal = 0;
                DiceToThrow = 6;
                string name = "";
                bool found = false;
                foreach (var user in FarkleMembers.Values)
                { //Get the next user in the list
                    if (found)
                    {
                        name = user;
                        return;
                    }
                    else if (user.Equals(m_addresser))
                    {
                        found = true;
                    }
                }
                if (name == "")
                { //If we were at the end of the list.
                    foreach (var user in FarkleMembers.Values)
                    {
                        name = user;
                        return;
                    }
                }
                m_users.SetFarkleToken(name, true);
                handler.Say(name + ", it is now your turn.");
            }

            if (!FarkleInSession)
                FarkleInSession = true;

        }

        public void JoinFarkle()
        {
            if (!FarkleInSession)
            {
                m_users.ClearFarkleScores();
                if (m_users.IsPlayingFarkle(m_addresser))
                {
                    handler.Say(m_addresser + " you are already playing farkle.", m_addresser);
                    return;
                }
                if (!m_users.IsPlayingFarkle())
                {
                    handler.Say(m_addresser + " has started a game of farkle, to join type " + ircBot.Settings.command_start + "joinfarkle");
                    m_users.SetFarkleToken(m_addresser, true);
                }
            }
            else
            {
                handler.Say(m_addresser + " you cannot join when a game is already being played.", m_addresser);
                return;
            }
            handler.Say(m_addresser + " welcome to this game of farkle.");
            FarkleMembers.Add(++NumOfFarkleMembers, m_addresser);
            m_users.SetFarkleFlag(m_addresser, true);
        }

        public void Help()
        {
            handler.Say("To play farkle you have the following commands:", m_addresser);
            handler.Say(ircBot.Settings.command_start + "farkle Rolls at the start of game play", m_addresser);
            handler.Say("and is used when all rolled dice are scoring.", m_addresser);
            handler.Say(ircBot.Settings.command_start + "farkle n #  Re-rolls # of dice, and keeps the highest of the rest.", m_addresser);
            handler.Say(ircBot.Settings.command_start + "farkle y Keeps all dice and ends your turn.", m_addresser);
            handler.Say(ircBot.Settings.command_start + "farklescore <player> Returns your score or players if provided.", m_addresser);
            handler.Say(ircBot.Settings.command_start + "joinfarkle Join a new game of farkle.", m_addresser);
            handler.Say(ircBot.Settings.command_start + "farklehelp This help information.", m_addresser);
            handler.Say(ircBot.Settings.command_start + "farkelforfeit To stop in the middle of a game.", m_addresser);
        }

        public void Score(string command, int endCommand)
        {
            if (command.Length == endCommand + 1)
                handler.Say(m_addresser + " your score is " + m_users.FarkleValue(m_addresser) + ".");
            else
            {
                string name = command.Substring(endCommand + 2).Trim();
                if (m_users.HasUser(name))
                    handler.Say(name + "'s score is " + m_users.FarkleValue(name) + ".");
                else
                {
                    handler.Say(m_addresser + " that user does not exist.");
                    return;
                }
            }
        }

        public void FarkleForfeit()
        {
            if (m_users.IsPlayingFarkle(m_addresser))
            {
                bool hasToken = m_users.GetFarkleToken(m_addresser);
                handler.Say(m_addresser + " forfeit.");

                bool found = false;
                string name = "";
                if (hasToken)
                {
                    foreach (string user in FarkleMembers.Values)
                    { //Get the next user in the list
                        if (found)
                        {
                            name = user;
                            return;
                        }
                        else if (user.Equals(m_addresser))
                        {
                            found = true;
                        }
                    }
                    if (name == "")
                    { //If we were at the end of the list.
                        foreach (string user in FarkleMembers.Values)
                        {
                            name = user;
                            return;
                        }
                    }
                    m_users.SetFarkleToken(name, true);
                    handler.Say(name + ", it is now your turn.");
                }

                m_users.SetFarkleFlag(m_addresser, false);
                m_users.SetFarkleToken(m_addresser, false);
                foreach (int member in FarkleMembers.Keys)
                {
                    if (FarkleMembers[member] == m_addresser)
                    {
                        FarkleMembers.Remove(member);
                        return;
                    }
                }
            }
        }

        bool FarkleDiceAllScoring()
        {
            foreach (int die in dice.Keys)
            {
                if (FarkleValueCheck(die, dice[die]) == 0)
                    return false;

                if (die != 1 && die != 5 && dice[die] % 3 != 0)
                    return false;

                if (dice.Count == 3 && !dice.ContainsValue(1) && !dice.ContainsValue(3))
                    return true;

                if (dice.Count == 6)
                    return true;
            }
            return true;
        }

        void FarkleRoll()
        {
            string infoOutput = null;
            int temp;
            if (m_users.IsPlayingFarkle(m_addresser) == false || !m_users.GetFarkleToken(m_addresser))
            {
                handler.Say(m_addresser + " please don't try and throw dice on someone elses turn, wait until the end.", m_addresser);
                return;
            }
            for (int i = 0; i < DiceToThrow; i++)
            {
                int tempValue;
                temp = (random.Next(1, 100) % 6) + 1;
                if (dice.ContainsKey(temp))
                {
                    dice.TryGetValue(temp, out tempValue);
                    dice[temp] = ++tempValue;
                }
                else
                {
                    dice.Add(temp, 1);
                }

                Task.Delay(temp * 50);
            }
            foreach (int outputdice in dice.Keys)
            {
                if (dice[outputdice] > 1)
                {
                    for (int i = 0; i < dice[outputdice]; i++)
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
                    handler.Say(m_addresser + " you rolled " + infoOutput + " for a total of " + (FarkleTotal + TempFarkleTotal) + ".");
                    handler.Say("You bust.");
                    bool found = false;
                    string name = "";
                    foreach (string user in FarkleMembers.Values)
                    { //Get the next user in the list
                        if (found)
                        {
                            name = user;
                            return;
                        }
                        else if (user.Equals(m_addresser))
                        {
                            found = true;
                        }
                    }
                    if (name == "")
                    { //If we were at the end of the list.
                        foreach (string user in FarkleMembers.Values)
                        {
                            name = user;
                            return;
                        }
                    }
                    m_users.SetFarkleToken(name, true);
                    handler.Say(name + ", it is now your turn.");
                    return;
                }
                else
                {
                    TempFarkleTotal = FarkleValueCheck(dice);
                }
            }
            handler.Say(m_addresser + " you rolled " + infoOutput + " for a total of " + (FarkleTotal + TempFarkleTotal) + ".");
        }
        static bool FarkleIsThreePairs(Dictionary<int, int> dice)
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

            return false;
        }

        static int FarkleValueCheck(Dictionary<int, int> dice)
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
        int FarkleValueCheck(int dieKey, int numToKeep)
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

        void DeclareWinner()
        {
            bool tie = false;
            string winner = m_addresser;
            foreach (string user in FarkleMembers.Values)
            {
                if (m_users.FarkleValue(user) >= 5000 && m_users.FarkleValue(user) > m_users.FarkleValue(winner))
                {
                    winner = user;
                }
                else if (m_users.FarkleValue(user) >= 5000 && m_users.FarkleValue(user) == m_users.FarkleValue(winner) && !user.Equals(winner))
                {
                    winner = user;
                    tie = true;
                }
            }
            if (tie)
            {
                handler.Say("This match ended in a tie between " + m_addresser + " and " + winner + " at "
                    + m_users.FarkleValue(winner) + " points!!!");
            }
            else
            {
                handler.Say(winner + " won with " + m_users.FarkleValue(winner) + " points!!!");
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
