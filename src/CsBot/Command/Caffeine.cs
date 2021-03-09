using System;

namespace CsBot.Command
{
    class Caffeine : iCommand
    {
        CommandHandler handler;

        public Caffeine(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void handle(string command, int endCommand, string verb)
        {
            if (verb != this.GetType().Name.ToLower()) return;

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
