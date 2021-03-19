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

            var addresser = handler.GetAddresser();
            var ircBot = handler.GetIrcBot();
            var users = handler.GetUsers();
            var random = new Random();
            var fromChannel = handler.GetFromChannel();

            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: What did you want {ircBot.Settings.Nick} to say?");
            }
            else
            {
                var toSay = command.Substring(endCommand + 2).Trim();
                if (toSay.StartsWith("in"))
                {
                    var channel = handler.GetChannel(toSay);
                    if (channel != null)
                    {
                        var toSayIn = toSay.Substring(toSay.IndexOf(channel) + channel.Length + 1);
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
