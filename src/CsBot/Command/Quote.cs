using System;
using System.Linq;

namespace CsBot.Command
{
    class Quote : ICommand
    {
	    readonly CommandHandler handler;
	    readonly Random random;

        public Quote (CommandHandler handler)
        {
            this.handler = handler;
            random = new Random();
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != GetType().Name.ToLower()) return;

            var quotes = handler.GetQuotes();

            if (quotes != null && quotes.Any ())
            {
                handler.Say(quotes[random.Next(0, 10000) % quotes.Count]);
            }
            else
            {
                handler.Say("Hey, the blues. The tragic sound of other people's suffering. Thant's kind of a pick-me-up.");
            }
        }
    }
}
