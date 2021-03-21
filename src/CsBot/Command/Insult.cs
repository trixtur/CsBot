using System;
using System.Linq;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class Insult : ICommand
    {
	    readonly CommandHandler _handler;

		public string Name { get; }

        public Insult(CommandHandler handler)
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
                _handler.Say($"{addresser}: Who do you want {ircBot.Settings.Nick} to insult?");
            }
            else
            {
                var toInsult = command.Substring(endCommand + 2).Trim();
                if (!users.HasUser(toInsult))
                {
                    _handler.Say($"{addresser}: That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine($"{addresser} insulted {toInsult}.");
                    if (ircBot.Settings.Insults != null && ircBot.Settings.Insults.Any())
                    {
                        _handler.Say(
	                        $"/me {string.Format(ircBot.Settings.Insults[random.Next(0, 10000) % ircBot.Settings.Insults.Count], toInsult)}");
                    }
                    else
                    {
                        _handler.Say($"/me thinks {toInsult} is screwier than his Aunt Rita, and she's a screw.");
                    }
                }
            }
        }
    }
}
