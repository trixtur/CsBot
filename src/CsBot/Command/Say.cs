using System;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class Say : ICommand
    {
	    readonly CommandHandler _handler;

	    public string Name { get; }


		public Say (CommandHandler handler)
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
                _handler.Say($"{addresser}: What did you want {ircBot.Settings.Nick} to say?");
            }
            else
            {
                var toSay = command.Substring(endCommand + 2).Trim();
                if (toSay.StartsWith("in"))
                {
                    var channel = _handler.GetChannel(toSay);
                    if (channel != null)
                    {
                        var toSayIn = toSay.Substring(toSay.IndexOf(channel) + channel.Length + 1);
                        _handler.Say(toSayIn, channel);
                    }
                }
                else
                {
                    _handler.Say(toSay, fromChannel);
                }
            }
        }
    }
}
