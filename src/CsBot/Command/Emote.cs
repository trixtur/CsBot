using System;

namespace CsBot.Command
{
    class Emote : ICommand
    {
	    readonly CommandHandler handler;

        public Emote(CommandHandler handler)
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
                handler.Say($"{addresser}: What did you want {ircBot.Settings.Nick} to emote?");
            }
            else
            {
                var toEmote = command.Substring(endCommand + 2).Trim();
                if (toEmote.StartsWith("in"))
                {
                    var channel = handler.GetChannel(toEmote);
                    if (channel != null)
                    {
                        var toEmoteIn = toEmote.Substring(toEmote.IndexOf(channel) + channel.Length + 1);
                        handler.Say($"/me {toEmoteIn}", channel);
                    }
                }
                else
                {
                    handler.Say($"/me {toEmote}");
                }
            }
        }
    }
}
