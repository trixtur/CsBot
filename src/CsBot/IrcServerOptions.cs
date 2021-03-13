using System.Collections.Generic;

namespace CsBot
{
    class IrcServerOptions
    {
        public string Server { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool Secure { get; set; }
        public bool ServerValidate { get; set; }
        public string User { get; set; }
        public string Nick { get; set; }
        public List<Channel> Channels { get; set; }
        public List<string> Admins { get; set; }
        public string CommandStart { get; set; }
        public List<string> Insults { get; set; }
        public List<string> Quotes { get; set; }
        public List<string> Praises { get; set; }
    }
}
