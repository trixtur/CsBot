using System;

namespace CsBot.Command
{
    class Praise : iCommand
    {
        CommandHandler handler;

        public Praise(CommandHandler handler)
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
                this.handler.Say(addresser + ": Who do you want " + ircBot.Settings.nick + " to praise?");
            }
            else
            {
                string toPraise = command.Substring(endCommand + 2).Trim();
                if (!users.HasUser(toPraise))
                {
                    this.handler.Say(addresser + ": That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine(addresser + " praised " + toPraise + ".");
                    if (ircBot.Settings.praises != null && ircBot.Settings.praises.Length > 0)
                    {
                        this.handler.Say("/me " + string.Format(ircBot.Settings.praises[random.Next(0, 10000) % ircBot.Settings.praises.Length], toPraise));
                    }
                    else
                    {
                        this.handler.Say("/me thinks " + toPraise + " is very smart.");
                    }
                }
            }
        }
    }
}
