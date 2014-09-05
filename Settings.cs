using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CsBot
{
    [DataContract]
    class Settings
    {
        [DataMember(Name="server", IsRequired=true)]
        public String server;

        [DataMember(Name="password", IsRequired=true)]
        public String password;

        [DataMember(Name="port", IsRequired=true)]
        public int port;

        [DataMember(Name="user", IsRequired=true)]
        public String user;

        [DataMember(Name="nick", IsRequired=true)]
        public String nick;

        [DataMember(Name="channels", IsRequired=true)]
        public List<Channel> channels;

        [DataMember(Name="admins", IsRequired=true)]
        public string[] admins;

        [DataMember(Name="command_start", IsRequired=true)]
        public String command_start;

        [DataMember(Name="insults", IsRequired=false)]
        public string[] insults;

        [DataMember(Name="quotes", IsRequired=false)]
        public string[] quotes;

    }

    [DataContract]
    class Channel
    {
        [DataMember(Name="id", IsRequired=true)]
        public int id;

        [DataMember(Name="name", IsRequired=true)]
        public string name;

        [DataMember(Name="key", IsRequired=false)]
        public string key;
    }
}
