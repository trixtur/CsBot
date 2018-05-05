﻿using System;

namespace CsBot.Games
{
    public enum RoShamBo { Rock, Paper, Scissors };

    class RockPaperScissors
    {
        CommandHandler handler;

        Users m_users => handler.m_users;
        string m_addresser => handler.m_addresser;
        IrcBot ircBot => handler.ircBot;

        public RockPaperScissors(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Play(CommandHandler commandHandler, string command, int endCommand)
        {
            if (command.Length == endCommand + 1 && m_users.RPSValue(m_addresser) == -2)
            {
                commandHandler.Say("/me whispers something to " + m_addresser + ".");
                commandHandler.Say("Would you like to throw rock, paper, or scissors?", m_addresser);
            }
            else if (m_users.RPSValue(m_addresser) == -2)
            {
                commandHandler.Say("Please just use " + ircBot.Settings.command_start + "rps as a single command. Thanks!");
            }

            string opponent;
            bool isPlaying = m_users.IsOpponentPlayingRPS(m_addresser, out opponent); //TODO: This needs to look for a player other than myself. Not the first person in the list.
            Console.WriteLine("isPlaying: " + isPlaying + " opponent: " + opponent);
            if (isPlaying && (!opponent.Equals(m_addresser)) && m_users.RPSValue(m_addresser) != -2 && m_users.RPSValue(opponent) != -2)
            {
                int opponent_throw = m_users.RPSValue(opponent);
                int my_throw = m_users.RPSValue(m_addresser);
                m_users.StopRPS(opponent);
                m_users.StopRPS(m_addresser);
                if (opponent_throw == my_throw)
                    commandHandler.Say("The Rock, Paper, Scissors game between " + opponent + " and " + m_addresser + " ended in a tie.");
                else if (opponent_throw == (int)RoShamBo.Rock && my_throw == (int)RoShamBo.Scissors)
                    commandHandler.Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Scissors && my_throw == (int)RoShamBo.Rock)
                    commandHandler.Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Paper && my_throw == (int)RoShamBo.Scissors)
                    commandHandler.Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Scissors && my_throw == (int)RoShamBo.Paper)
                    commandHandler.Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Paper && my_throw == (int)RoShamBo.Rock)
                    commandHandler.Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Rock && my_throw == (int)RoShamBo.Paper)
                    commandHandler.Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
            }
            else if (opponent.Equals(string.Empty) && !m_users.IsPlayingRPS(m_addresser))
            {
                commandHandler.Say(m_addresser + " is looking for an opponent in Rock, Paper, Scissors.");
            }

        }
    }
}
