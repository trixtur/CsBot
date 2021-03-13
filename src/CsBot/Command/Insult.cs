using System;

namespace CsBot.Command
{
    class Insult : ICommand
    {
	    readonly CommandHandler handler;

        public Insult(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != GetType().Name.ToLower()) return;

            var addresser = handler.GetAddresser();
            var ircBot = handler.GetIrcBot();
            var users = handler.GetUsers();
            var random = new Random();

            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: Who do you want {ircBot.Settings.Nick} to insult?");
            }
            else
            {
                var toInsult = command.Substring(endCommand + 2).Trim();
                if (!users.HasUser(toInsult))
                {
                    handler.Say($"{addresser}: That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine($"{addresser} insulted {toInsult}.");
                    if (ircBot.Settings.Insults != null && ircBot.Settings.Insults.Length > 0)
                    {
                        handler.Say(
	                        $"/me {string.Format(ircBot.Settings.Insults[random.Next(0, 10000) % ircBot.Settings.Insults.Length], toInsult)}");
                    }
                    else
                    {
                        handler.Say($"/me thinks {toInsult} is screwier than his Aunt Rita, and she's a screw.");
                    }
                }
            }
        }
    }
}
