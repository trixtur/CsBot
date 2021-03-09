using System;

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

            string addresser = handler.GetAddresser();
            IrcBot ircBot = handler.GetIrcBot();
            Users users = handler.GetUsers();
            Random random = new Random();

            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: Who do you want {ircBot.Settings.nick} to praise?");
                return;
            }

            string toPraise = command.Substring(endCommand + 2).Trim();
            if (!users.HasUser(toPraise))
            {
                handler.Say($"{addresser}: That person doesn't exist.");
            }
            else
            {
                Console.WriteLine($"{addresser} praised {toPraise}.");
                if (ircBot.Settings.praises != null && ircBot.Settings.praises.Length > 0)
                {
                    handler.Say($"/me {string.Format(ircBot.Settings.praises[random.Next(0, 10000) % ircBot.Settings.praises.Length], toPraise)}");
                }
                else
                {
                    handler.Say($"/me thinks {toPraise} is very smart.");
                }
            }
        }
    }
}
