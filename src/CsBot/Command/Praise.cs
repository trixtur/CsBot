using System;
using System.Linq;

namespace CsBot.Command
{
    class Praise : ICommand
    {
	    readonly CommandHandler handler;

        public Praise(CommandHandler handler)
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
                handler.Say($"{addresser}: Who do you want {ircBot.Settings.Nick} to praise?");
                return;
            }

            var toPraise = command.Substring(endCommand + 2).Trim();
            if (!users.HasUser(toPraise))
            {
                handler.Say($"{addresser}: That person doesn't exist.");
            }
            else
            {
                Console.WriteLine($"{addresser} praised {toPraise}.");
                if (ircBot.Settings.Praises != null && ircBot.Settings.Praises.Any ())
                {
                    handler.Say($"/me {string.Format(ircBot.Settings.Praises[random.Next(0, 10000) % ircBot.Settings.Praises.Count], toPraise)}");
                }
                else
                {
                    handler.Say($"/me thinks {toPraise} is very smart.");
                }
            }
        }
    }
}
