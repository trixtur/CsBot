using System;

namespace CsBot.Command
{
    class Caffeine
    {
        CommandHandler handler;
        bool shouldRun;

        public Caffeine(CommandHandler handler, string command)
        {
            this.handler = handler;

            shouldRun = command == this.GetType().Name.ToLower();
        }

        public void handle(string command, int endCommand)
        {
            if (!shouldRun) return;

            string addresser = this.handler.GetAddresser();
            IrcBot ircBot = this.handler.GetIrcBot();
            Users users = this.handler.GetUsers();
            Random random = new Random();

            if (command.Length == endCommand + 1)
            {
                this.handler.Say("/me walks over to " + addresser + " and gives them a shot of caffeine straight into the blood stream.");
            }
            else
            {
                int shots;
                if (!int.TryParse(command.Substring(endCommand + 2).Trim(), out shots))
                {
                    this.handler.Say(addresser + ": I didn't understand, how many shots of caffeine did you want?");
                }
                else if (shots == 1)
                {
                    this.handler.Say("/me walks over to " + addresser + " and gives them a shot of caffeine straight into the blood stream.");
                }
                else
                {
                    this.handler.Say("/me walks over to " + addresser + " and gives them " + shots + " shots of caffeine straight into the blood stream.");
                }
            }
        }
    }
}
