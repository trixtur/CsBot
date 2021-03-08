using System;

namespace CsBot.Command
{
    class Say : iCommand
    {
        CommandHandler handler;

        public Say(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void handle(string command, int endCommand, string verb)
        {
            if (verb != this.GetType().Name.ToLower()) return;

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
