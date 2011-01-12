using System;
using System.Collections.Generic;
using System.Text;

namespace CsBot
{
    class users
    {
        private Dictionary<string, user> m_users;

        public users()
        {
            m_users = new System.Collections.Generic.Dictionary<string, user>();
        }

        public bool hasUser(string user)
        {
            return m_users.ContainsKey(user);
        }

        public void addUser(string user)
        {
            user tmpUser = new user();
            tmpUser.Name(user);
            if (!m_users.ContainsKey(user))
                m_users.Add(user, tmpUser);
        }

        public bool isPlayingRPS(out string playing_user)
        {
            Dictionary<string, user>.KeyCollection.Enumerator e = m_users.Keys.GetEnumerator();
            e.MoveNext();
            for (int i = 0; i < m_users.Count; i++)
            {
                if (m_users[e.Current].rps_flag)
                {
                    playing_user = e.Current;
                    return true;
                }
                e.MoveNext();
            }
            playing_user = string.Empty;
            return false;
        }

        public bool isPlayingRPS(string current_user)
        {
            return m_users[current_user].rps_flag;
        }

        public void RPSValue(string player, int value)
        {
            if (m_users.ContainsKey(player))
            {
                m_users[player].rps_flag = true;
                m_users[player].RPS(value);
            }
        }

        public int RPSValue(string player)
        {
            if (m_users.ContainsKey(player))
            {
                return m_users[player].RPS();
            }
            return -1;
        }

        public void addUserLastMessage(string uname, string message)
        {
            m_users[uname].Message(message);
        }

        public void StopRPS(string uname)
        {
            m_users[uname].RPS(-2);
            m_users[uname].rps_flag = false;
        }

        public string getUserMessage(string uname)
        {
            return m_users[uname].Message();
        }
    }
}
