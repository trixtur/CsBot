using System.Collections.Generic;

namespace CsBot
{
    class Settings
    {
        public string Server { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Secure { get; set; }
        public bool ServerValidate { get; set; }
        public string User { get; set; }
        public string Nick { get; set; }
        public List<Channel> Channels { get; set; }
        public string[] Admins { get; set; }
        public string CommandStart { get; set; }
        public string[] Insults { get; set; }
        public string[] Quotes { get; set; }
        public string[] Praises { get; set; }
    }
}
