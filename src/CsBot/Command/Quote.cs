using System;

namespace CsBot.Command
{
    class Quote : iCommand
    {
        CommandHandler handler;
        Random random;

        public Quote (CommandHandler handler)
        {
            this.handler = handler;
            this.random = new Random();
        }

        public void handle(string command, int endCommand, string verb)
        {
            if (verb != this.GetType().Name.ToLower()) return;

            string[] quotes = this.handler.GetQuotes();

            if (quotes != null && quotes.Length > 0)
            {
                this.handler.Say(quotes[this.random.Next(0, 10000) % quotes.Length]);
            }
            else
            {
                this.handler.Say("Hey, the blues. The tragic sound of other people's suffering. Thant's kind of a pick-me-up.");
            }
        }
    }
}
