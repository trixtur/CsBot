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

            string addresser = handler.GetAddresser();
            IrcBot ircBot = handler.GetIrcBot();
            Users users = handler.GetUsers();
            Random random = new Random();
            string fromChannel = handler.GetFromChannel();

            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: What did you want {ircBot.Settings.nick} to emote?");
            }
            else
            {
                string toEmote = command.Substring(endCommand + 2).Trim();
                if (toEmote.StartsWith("in"))
                {
                    string channel = handler.GetChannel(toEmote);
                    if (channel != null)
                    {
                        string toEmoteIn = toEmote.Substring(toEmote.IndexOf(channel) + channel.Length + 1);
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
