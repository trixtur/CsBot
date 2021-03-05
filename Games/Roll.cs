using System;

namespace CsBot.Games
{
    class Roll
    {
        CommandHandler handler;
        int d1, d2, total;
        readonly int DICE = 6;
        Random random;
        bool shouldRun;

        public Roll(CommandHandler handler, string command)
        {
            this.handler = handler;
            random = new Random();

            shouldRun = command == this.GetType().Name.ToLower();
        }

        public void Play(string command, int endCommand)
        {
            if (!shouldRun) return;

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
