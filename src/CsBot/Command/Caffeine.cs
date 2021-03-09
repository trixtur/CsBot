using System;

namespace CsBot.Command
{
    class Caffeine : ICommand
    {
	    readonly CommandHandler handler;

        public Caffeine(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != GetType().Name.ToLower()) return;

            string addresser = handler.GetAddresser();
            IrcBot ircBot = handler.GetIrcBot();
            Users users = handler.GetUsers();
            Random random = new Random();

            if (command.Length == endCommand + 1)
            {
                handler.Say($"/me walks over to {addresser} and gives them a shot of caffeine straight into the blood stream.");
            }
            else
            {
	            if (!int.TryParse(command.Substring(endCommand + 2).Trim(), out var shots))
                {
                    handler.Say($"{addresser}: I didn't understand, how many shots of caffeine did you want?");
                }
                else if (shots == 1)
                {
                    handler.Say($"/me walks over to {addresser} and gives them a shot of caffeine straight into the blood stream.");
                }
                else
                {
                    handler.Say($"/me walks over to {addresser} and gives them {shots} shots of caffeine straight into the blood stream.");
                }
            }
        }
    }
}
