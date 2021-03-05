using System;

namespace CsBot.Command
{
    class Quote
    {
        CommandHandler handler;
        Random random;
        bool shouldRun;

        public Quote (CommandHandler handler, string command)
        {
            this.handler = handler;
            this.random = new Random();

            shouldRun = command == this.GetType().Name.ToLower();
        }

        public void handle(string command, int endCommand)
        {
            if (!shouldRun) return;

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
