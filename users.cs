using System;
using System.Collections.Generic;

namespace CsBot
{
    class Users
    {
        readonly Dictionary<string, User> m_users;

        public Users()
        {
            m_users = new Dictionary<string, User>();
        }

        public bool HasUser(string user)
        {
            return m_users.ContainsKey(user);
        }

        public void Remove (string user)
        {
            if (m_users.ContainsKey(user))
                m_users.Remove(user);
        }

        public void Add(string user, User tmpUser = null)
        {
            if (tmpUser == null) {
                tmpUser = new User { Name = user };
            }

            if (!m_users.ContainsKey(user))
                m_users.Add(user, tmpUser);
        }

        public bool IsPlayingRPS(string current_user)
        {
            if (m_users.ContainsKey(current_user))
                return m_users[current_user].RPSFlag;

            return false;
        }

        public bool IsOpponentPlayingRPS(string addresser, out string playing_user) {
            Dictionary<string, User>.KeyCollection.Enumerator e = m_users.Keys.GetEnumerator();
            e.MoveNext();
            for (int i = 0; i < m_users.Count; i++)
            {
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


        public bool IsPlayingFarkle(string current_user)
        {
            if (m_users.ContainsKey(current_user))
                return m_users[current_user].FarkleFlag;

            return false;
        }

        public void ClearFarkleScores()
        {
            foreach (var user in m_users.Keys)
                FarkleValue(user, -FarkleValue(user));
        }

        public bool IsPlayingFarkle()
        {
            bool ans = false;

            foreach (var user in m_users.Keys)
            {
                ans = m_users[user].FarkleFlag;
                if (ans)
                    break;
            }
            return ans;
        }

        public bool SomeoneHasToken()
        {
            bool ans = false;
            foreach (string m in m_users.Keys)
            {
                ans = m_users[m].HasFarkleToken;
                if (ans)
                    break;
            }
            return ans;
        }

        public void SetFarkleFlag(string player, bool playing)
        {
            if (m_users.ContainsKey(player))
                m_users[player].FarkleFlag = playing;
        }

        public void FarkleValue(string player, int value)
        {
            if (m_users.ContainsKey(player))
                m_users[player].FarkleValue = value;
        }

        public int FarkleValue(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users[player].FarkleValue;

            return 0;
        }

        public void SetFarkleToken(string player, bool value)
        {
            if (m_users.ContainsKey(player))
                m_users[player].HasFarkleToken = value;
        }

        public bool GetFarkleToken(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users[player].HasFarkleToken;

            return false;
        }

        public void RPSValue(string player, int value)
        {
            if (!m_users.ContainsKey(player)) return;

            m_users[player].RPSFlag = true;
            m_users[player].RPS = value;
        }

        public int RPSValue(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users[player].RPS;

            return -1;
        }

        public void AddUserLastMessage(string uname, string message)
        {
            m_users[uname].Message = message;
        }

        public void StopRPS(string uname)
        {
            m_users[uname].RPS = -2;
            m_users[uname].RPSFlag = false;
        }

        public string getUserMessage(string uname)
        {
            return m_users[uname].Message;
        }

        public User this[string name] => m_users[name];

        public void PrintUsers()
        {
            foreach (var user in m_users.Keys)
                Console.WriteLine("Users  " + user);
        }
    }
}
