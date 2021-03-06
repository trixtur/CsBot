using System;

namespace CsBot.Command
{
    class APB : iCommand
    {
        CommandHandler handler;

        public APB(CommandHandler handler)
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
                this.handler.Say(addresser + ": Who do you want " + ircBot.Settings.nick + " to find?");
            }
            else
            {
                string toFind = command.Substring(endCommand + 2).Trim();
                if (!users.HasUser(toFind))
                {
                    this.handler.Say(addresser + ": That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine(addresser + " put out apb for " + toFind);
                    this.handler.Say("/me sends out the blood hounds to find " + toFind + ".");
                }
            }
        }
    }
}
