using System;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class Emote : ICommand
    {
	    readonly CommandHandler _handler;

		public string Name { get; }

        public Emote(CommandHandler handler)
        {
            _handler = handler;

            Name = GetType ().Name;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != Name.ToLower()) return;

            var addresser = _handler.GetAddresser();
            var ircBot = _handler.GetIrcBot();
            var users = _handler.GetUsers();
            var random = new Random();
            var fromChannel = _handler.GetFromChannel();

            if (command.Length == endCommand + 1)
            {
                _handler.Say($"{addresser}: What did you want {ircBot.Settings.Nick} to emote?");
            }
            else
            {
                var toEmote = command.Substring(endCommand + 2).Trim();
                if (toEmote.StartsWith("in"))
                {
                    var channel = _handler.GetChannel(toEmote);
                    if (channel != null)
                    {
                        var toEmoteIn = toEmote.Substring(toEmote.IndexOf(channel) + channel.Length + 1);
                        _handler.Say($"/me {toEmoteIn}", channel);
                    }
                }
                else
                {
                    _handler.Say($"/me {toEmote}");
                }
            }
        }
    }
}
