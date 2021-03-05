using System;

namespace CsBot.Command
{
    class Praise
    {
        CommandHandler handler;
        bool shouldRun;

        public Praise(CommandHandler handler, string command)
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
