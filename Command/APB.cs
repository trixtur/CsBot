using System;

namespace CsBot.Command
{
    class APB
    {
        CommandHandler handler;
        bool shouldRun;

        public APB(CommandHandler handler, string command)
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
