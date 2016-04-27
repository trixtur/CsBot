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

        public void removeUser(string user)
        {
            m_users.Remove(user);
        }

        public void addUser(string user, user tmpUser = null)
        {
            if (tmpUser == null) {
                tmpUser = new user();
                tmpUser.Name = user;
            }

            if (!m_users.ContainsKey(user))
                m_users.Add(user, tmpUser);
        }

        public bool isPlayingRPS(out string playing_user)
        {
            Dictionary<string, user>.KeyCollection.Enumerator e = m_users.Keys.GetEnumerator();
            e.MoveNext();
            for (int i = 0; i < m_users.Count; i++)
            {
                if (m_users[e.Current].RPSFlag)
                {
                    playing_user = e.Current;
                    return true;
                }
                e.MoveNext();
            }
            playing_user = string.Empty;
            return false;
        }

        public bool isOpponentPlayingRPS(string addresser, out string playing_user) {
            Dictionary<string, user>.KeyCollection.Enumerator e = m_users.Keys.GetEnumerator();
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

        public bool isPlayingRPS(string current_user)
        {
            return m_users[current_user].RPSFlag;
        }

        public bool isPlayingFarkle(string current_user)
        {
            return m_users[current_user].FarkleFlag;
        }

        public void ClearFarkleScores()
        {
            foreach (string m in m_users.Keys)
            {
                FarkleValue(m, -FarkleValue(m));
            }
        }

        public bool isPlayingFarkle()
        {
            bool ans = false;
            foreach (string m in m_users.Keys)
            {
                ans = m_users[m].FarkleFlag;
                if (ans == true)
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
                if (ans == true)
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
            {
                m_users[player].FarkleValue = value;
            }
        }

        public int FarkleValue(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users[player].FarkleValue;
            else
                return 0;
        }

        public void SetFarkleToken(string player, bool value)
        {
            if (m_users.ContainsKey(player)) {
                m_users[player].HasFarkleToken = value;
            } else {
                return;
            }
        }

        public bool GetFarkleToken(string player)
        {
            if (m_users.ContainsKey(player))
                return m_users[player].HasFarkleToken;
            else
                return false;
        }

        public void RPSValue(string player, int value)
        {
            if (m_users.ContainsKey(player))
            {
                m_users[player].RPSFlag = true;
                m_users[player].RPS = value;
            }
        }

        public int RPSValue(string player)
        {
            if (m_users.ContainsKey(player))
            {
                return m_users[player].RPS;
            }
            return -1;
        }

        public void addUserLastMessage(string uname, string message)
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

        public IEnumerator<string> GetEnumerator()
        {
            Dictionary<string, user>.KeyCollection.Enumerator e = m_users.Keys.GetEnumerator();
            return e;
        }

        public user this[string name]
        {
            get
            {
                return m_users[name];
            }
            set
            {
                return; //Don't allow
            }
        }
    }
}
