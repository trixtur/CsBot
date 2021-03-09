using System;
using System.Collections.Generic;

namespace CsBot.Games
{
    public enum RoShamBo { Rock, Paper, Scissors };

    class RockPaperScissors : iGame
    {
        CommandHandler handler;
        private const string GameName = "rps";

        Users m_users => handler.m_users;
        string m_addresser => handler.m_addresser;
        IrcBot ircBot => handler.ircBot;

        public RockPaperScissors(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Play(string command, int endCommand, string verb)
        {
            if (verb != GameName) return;

            if (command.Length == endCommand + 1 && RPSValue(m_addresser) == -2)
            {
                this.handler.Say("/me whispers something to " + m_addresser + ".");
                this.handler.Say("Would you like to throw rock, paper, or scissors?", m_addresser);
            }
            else if (RPSValue(m_addresser) == -2)
            {
                this.handler.Say("Please just use " + ircBot.Settings.command_start + "rps as a single command. Thanks!");
            }

            string opponent;
            bool isPlaying = IsOpponentPlayingRPS(m_addresser, out opponent); 
            Console.WriteLine("isPlaying: " + isPlaying + " opponent: " + opponent);
            if (isPlaying && (!opponent.Equals(m_addresser)) && RPSValue(m_addresser) != -2 && RPSValue(opponent) != -2)
            {
                int opponent_throw = RPSValue(opponent);
                int my_throw = RPSValue(m_addresser);
                StopRPS(opponent);
                StopRPS(m_addresser);
                if (opponent_throw == my_throw)
                    this.handler.Say("The Rock, Paper, Scissors game between " + opponent + " and " + m_addresser + " ended in a tie.");
                else if (opponent_throw == (int)RoShamBo.Rock && my_throw == (int)RoShamBo.Scissors)
                    this.handler.Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Scissors && my_throw == (int)RoShamBo.Rock)
                    this.handler.Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Paper && my_throw == (int)RoShamBo.Scissors)
                    this.handler.Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Scissors && my_throw == (int)RoShamBo.Paper)
                    this.handler.Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Paper && my_throw == (int)RoShamBo.Rock)
                    this.handler.Say(opponent + " has beaten " + m_addresser + " at a game of Rock, Paper, Scissors.");
                else if (opponent_throw == (int)RoShamBo.Rock && my_throw == (int)RoShamBo.Paper)
                    this.handler.Say(m_addresser + " has beaten " + opponent + " at a game of Rock, Paper, Scissors.");
            }
            else if (opponent.Equals(string.Empty) && !IsPlayingRPS(m_addresser))
            {
                this.handler.Say(m_addresser + " is looking for an opponent in Rock, Paper, Scissors.");
            }

        }

        public void RPSValue(string player, int value)
        { 
            if (m_users.ContainsKey(player))
            {
                m_users.getUserByKey(player).RPSFlag = true;
                m_users.getUserByKey(player).RPS = value;
            }
	    }

        public int RPSValue(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users.getUserByKey(player).RPS;

            return -1;
        }

        public bool IsPlayingRPS(string current_user)
        {
            if (m_users.ContainsKey(current_user))
                return m_users.getUserByKey(current_user).RPSFlag;

            return false;
        }

        public bool IsOpponentPlayingRPS(string addresser, out string playing_user) {
            Dictionary<string, User>.KeyCollection.Enumerator e = m_users.GetEnumerator();
            Console.WriteLine("Current: {0}", e.Current);
            e.MoveNext();
            for (int i = 0; i < m_users.GetCount(); i++)
            {
                Console.WriteLine("Current: {0} is playing {1}.", e.Current, m_users[e.Current].RPSFlag);
                if (m_users[e.Current].RPSFlag && e.Current != addresser)
                {
                    playing_user = e.Current;
                    return true;
                }
                e.MoveNext();
            }
            playing_user = string.Empty;
            return false;
        }

        public void StopRPS(string uname)
        {
            m_users.getUserByKey(uname).RPS = -2;
            m_users.getUserByKey(uname).RPSFlag = false;
        }
    }
}
