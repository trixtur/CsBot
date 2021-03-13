using System;

namespace CsBot.Games
{
    public enum RoShamBo { Rock, Paper, Scissors };

    class RockPaperScissors : IGame
    {
	    readonly CommandHandler handler;
        const string GameName = "rps";

        Users m_users => handler.Users;
        string m_addresser => handler.Addresser;
        IrcBot ircBot => handler.IrcBot;

        public RockPaperScissors(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Play(string command, int endCommand, string verb)
        {
            if (verb != GameName) return;

            if (command.Length == endCommand + 1 && RPSValue(m_addresser) == -2)
            {
                handler.Say($"/me whispers something to {m_addresser}.");
                handler.Say("Would you like to throw rock, paper, or scissors?", m_addresser);
            }
            else if (RPSValue(m_addresser) == -2)
            {
                handler.Say($"Please just use {ircBot.Settings.CommandStart}rps as a single command. Thanks!");
            }

            var isPlaying = IsOpponentPlayingRPS(m_addresser, out var opponent); 
            Console.WriteLine($"isPlaying: {isPlaying} opponent: {opponent}");
            if (isPlaying && (!opponent.Equals(m_addresser)) && RPSValue(m_addresser) != -2 && RPSValue(opponent) != -2)
            {
                var opponentThrow = RPSValue(opponent);
                var myThrow = RPSValue(m_addresser);
                StopRPS(opponent);
                StopRPS(m_addresser);
                if (opponentThrow == myThrow)
                    handler.Say($"The Rock, Paper, Scissors game between {opponent} and {m_addresser} ended in a tie.");
                else if (opponentThrow == (int)RoShamBo.Rock && myThrow == (int)RoShamBo.Scissors)
                    handler.Say($"{opponent} has beaten {m_addresser} at a game of Rock, Paper, Scissors.");
                else if (opponentThrow == (int)RoShamBo.Scissors && myThrow == (int)RoShamBo.Rock)
                    handler.Say($"{m_addresser} has beaten {opponent} at a game of Rock, Paper, Scissors.");
                else if (opponentThrow == (int)RoShamBo.Paper && myThrow == (int)RoShamBo.Scissors)
                    handler.Say($"{m_addresser} has beaten {opponent} at a game of Rock, Paper, Scissors.");
                else if (opponentThrow == (int)RoShamBo.Scissors && myThrow == (int)RoShamBo.Paper)
                    handler.Say($"{opponent} has beaten {m_addresser} at a game of Rock, Paper, Scissors.");
                else if (opponentThrow == (int)RoShamBo.Paper && myThrow == (int)RoShamBo.Rock)
                    handler.Say($"{opponent} has beaten {m_addresser} at a game of Rock, Paper, Scissors.");
                else if (opponentThrow == (int)RoShamBo.Rock && myThrow == (int)RoShamBo.Paper)
                    handler.Say($"{m_addresser} has beaten {opponent} at a game of Rock, Paper, Scissors.");
            }
            else if (opponent.Equals(string.Empty) && !IsPlayingRPS(m_addresser))
            {
                handler.Say($"{m_addresser} is looking for an opponent in Rock, Paper, Scissors.");
            }
        }

        public void RPSValue(string player, int value)
        { 
            if (m_users.ContainsKey(player))
            {
                m_users.GetUserByKey(player).RPSFlag = true;
                m_users.GetUserByKey(player).RPS = value;
            }
	    }

        public int RPSValue(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users.GetUserByKey(player).RPS;

            return -1;
        }

        public bool IsPlayingRPS(string current_user)
        {
            if (m_users.ContainsKey(current_user))
                return m_users.GetUserByKey(current_user).RPSFlag;

            return false;
        }

        public bool IsOpponentPlayingRPS(string addresser, out string playing_user)
        {
	        using var e = m_users.GetEnumerator();
            Console.WriteLine($"Current: {e.Current}");
            e.MoveNext();
            for (var i = 0; i < m_users.GetCount(); i++)
            {
                Console.WriteLine($"Current: {e.Current} is playing {m_users[e.Current].RPSFlag}.");
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
            m_users.GetUserByKey(uname).RPS = -2;
            m_users.GetUserByKey(uname).RPSFlag = false;
        }
    }
}
