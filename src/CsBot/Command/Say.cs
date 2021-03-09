using System;

namespace CsBot.Command
{
    class Say : ICommand
    {
	    readonly CommandHandler handler;

        public Say(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != GetType().Name.ToLower()) return;

            string addresser = handler.GetAddresser();
            IrcBot ircBot = handler.GetIrcBot();
            Users users = handler.GetUsers();
            Random random = new Random();
            string fromChannel = handler.GetFromChannel();

            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: What did you want {ircBot.Settings.nick} to say?");
            }
            else
            {
                string toSay = command.Substring(endCommand + 2).Trim();
                if (toSay.StartsWith("in"))
                {
                    string channel = handler.GetChannel(toSay);
                    if (channel != null)
                    {
                        string toSayIn = toSay.Substring(toSay.IndexOf(channel) + channel.Length + 1);
                        handler.Say(toSayIn, channel);
                    }
                }
                else
                {
                    handler.Say(toSay, fromChannel);
                }
            }
        }
    }
}
