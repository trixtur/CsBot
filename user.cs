using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Syndication;

namespace CsBot
{
    class user
    {
        string m_name;
        string m_last_msg;
        int rps_value = -2;
        bool rps_flag = false;
        bool farkle_flag = false;
        bool has_farkel_token = false;
        int farkle_value = 0;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Message
        {
            get { return m_last_msg; }
            set { m_last_msg = value; }
        }
        public int RPS
        {
            get { return rps_value; }
            set { rps_value = value; }
        }
        public bool RPSFlag
        {
            get { return rps_flag; }
            set { rps_flag = value; }
        }
        public int FarkleValue
        {
            get { return farkle_value; }
            set { farkle_value += value; }
        }
        public bool FarkleFlag
        {
            get { return farkle_flag; }
            set { farkle_flag = value; }
        }
        public bool HasFarkleToken
        {
            get { return has_farkel_token; }
            set { has_farkel_token = value; }
        }
        public SyndicationFeed Feed { get; set; }
        public int FeedCount { get; set; }
    }
}
