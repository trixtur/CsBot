using System;

namespace CsBot.Games
{
    class Roll : IGame
    {
	    readonly CommandHandler handler;
        int d1, d2, total;
        readonly int DICE = 6;
        readonly Random random;

        public Roll(CommandHandler handler)
        {
            this.handler = handler;
            random = new Random();
        }

        public void Play(string command, int endCommand, string verb)
        {
            if (verb != GetType().Name.ToLower()) return;

            string addresser = handler.GetAddresser();

            d1 = random.Next(1, DICE + 1);
            d2 = random.Next(1, DICE + 1);

            total = d1 + d2;

            handler.Say($"{addresser} rolled a {d1} and a {d2} for a total of {total}"
            );
        }
    }
}
