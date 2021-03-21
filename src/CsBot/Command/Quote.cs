using System;
using System.Linq;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class Quote : ICommand
    {
	    readonly CommandHandler _handler;
	    readonly Random random;

	    public string Name { get; }

		public Quote (CommandHandler handler)
        {
            _handler = handler;
            random = new Random();

            Name = GetType ().Name;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != Name.ToLower()) return;

            var quotes = _handler.GetQuotes();

            if (quotes != null && quotes.Any ())
            {
                _handler.Say(quotes[random.Next(0, 10000) % quotes.Count]);
            }
            else
            {
                _handler.Say("Hey, the blues. The tragic sound of other people's suffering. That's kind of a pick-me-up.");
            }
        }
    }
}
