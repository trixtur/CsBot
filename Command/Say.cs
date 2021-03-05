using System;

namespace CsBot.Command
{
    class Say
    {
        CommandHandler handler;
        bool shouldRun;

        public Say(CommandHandler handler, string command)
        {
            this.handler = handler;

            shouldRun = command == this.GetType().Name.ToLower();
        }

        public void handle(string command, int endCommand)
        {
            if (!shouldRun) return;

            string addresser = this.handler.GetAddresser();
            IrcBot ircBot = this.handler.GetIrcBot();
            Users users = this.handler.GetUsers();
            Random random = new Random();
            string fromChannel = this.handler.GetFromChannel();

            if (command.Length == endCommand + 1)
            {
                this.handler.Say(addresser + ": What did you want " + ircBot.Settings.nick + " to say?");
            }
            else
            {
                string toSay = command.Substring(endCommand + 2).Trim();
                if (toSay.StartsWith("in"))
                {
                    string channel = this.handler.GetChannel(toSay);
                    if (channel != null)
                    {
                        string toSayIn = toSay.Substring(toSay.IndexOf(channel) + channel.Length + 1);
                        this.handler.Say(toSayIn, channel);
                    }
                }
                else
                {
                    this.handler.Say(toSay, fromChannel);
                }
            }
        }
    }
}
