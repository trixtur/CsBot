using System;

namespace CsBot.Command
{
    class Insult
    {
        CommandHandler handler;

        public Insult(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void handle(string command, int endCommand)
        {
            string addresser = this.handler.GetAddresser();
            IrcBot ircBot = this.handler.GetIrcBot();
            Users users = this.handler.GetUsers();
            Random random = new Random();

            if (command.Length == endCommand + 1)
            {
                this.handler.Say(addresser + ": Who do you want " + ircBot.Settings.nick + " to insult?");
            }
            else
            {
                string toInsult = command.Substring(endCommand + 2).Trim();
                if (!users.HasUser(toInsult))
                {
                    this.handler.Say(addresser + ": That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine(addresser + " insulted " + toInsult + ".");
                    if (ircBot.Settings.insults != null && ircBot.Settings.insults.Length > 0)
                    {
                        this.handler.Say("/me " + string.Format(ircBot.Settings.insults[random.Next(0, 10000) % ircBot.Settings.insults.Length], toInsult));
                    }
                    else
                    {
                        this.handler.Say("/me thinks " + toInsult + " is screwier than his Aunt Rita, and she's a screw.");
                    }
                }
            }
        }
    }
}
