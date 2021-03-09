using System.Collections.Generic;

namespace CsBot
{
    class Settings
    {
        public string server;
        public string password;
        public int port;
        public string secure;
        public bool server_validate;
        public string user;
        public string nick;
        public List<Channel> channels;
        public string[] admins;
        public string command_start;
        public string[] insults;
        public string[] quotes;
        public string[] praises;
    }
}
