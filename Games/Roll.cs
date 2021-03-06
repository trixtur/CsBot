using System;

namespace CsBot.Games
{
    class Roll : iGame
    {
        CommandHandler handler;
        int d1, d2, total;
        readonly int DICE = 6;
        Random random;

        public Roll(CommandHandler handler)
        {
            this.handler = handler;
            random = new Random();
        }

        public void Play(string command, int endCommand, string verb)
        {
            if (verb != this.GetType().Name.ToLower()) return;

            string addresser = this.handler.GetAddresser();

            this.d1 = random.Next(1, DICE + 1);
            this.d2 = random.Next(1, DICE + 1);

            total = d1 + d2;

            handler.Say(addresser + 
                    " rolled a " + d1 + 
                    " and a " + d2 + 
                    " for a total of " + total
            );
        }
    }
}
