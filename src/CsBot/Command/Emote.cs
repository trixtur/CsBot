using System;

namespace CsBot.Command
{
    class Emote : iCommand
    {
        CommandHandler handler;

        public Emote(CommandHandler handler)
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
                this.handler.Say(addresser + ": What did you want " + ircBot.Settings.nick + " to emote?");
            }
            else
            {
                string toEmote = command.Substring(endCommand + 2).Trim();
                if (toEmote.StartsWith("in"))
                {
                    string channel = this.handler.GetChannel(toEmote);
                    if (channel != null)
                    {
                        string toEmoteIn = toEmote.Substring(toEmote.IndexOf(channel) + channel.Length + 1);
                        this.handler.Say("/me " + toEmoteIn, channel);
                    }
                }
                else
                {
                    this.handler.Say("/me " + toEmote);
                }
            }
        }
    }
}
