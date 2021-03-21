using System;
using System.Linq;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class Praise : ICommand
    {
	    readonly CommandHandler _handler;

	    public string Name { get; }

		public Praise (CommandHandler handler)
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

            if (command.Length == endCommand + 1)
            {
                _handler.Say($"{addresser}: Who do you want {ircBot.Settings.Nick} to praise?");
                return;
            }

            var toPraise = command.Substring(endCommand + 2).Trim();
            if (!users.HasUser(toPraise))
            {
                _handler.Say($"{addresser}: That person doesn't exist.");
            }
            else
            {
                Console.WriteLine($"{addresser} praised {toPraise}.");
                if (ircBot.Settings.Praises != null && ircBot.Settings.Praises.Any ())
                {
                    _handler.Say($"/me {string.Format(ircBot.Settings.Praises[random.Next(0, 10000) % ircBot.Settings.Praises.Count], toPraise)}");
                }
                else
                {
                    _handler.Say($"/me thinks {toPraise} is very smart.");
                }
            }
        }
    }
}
