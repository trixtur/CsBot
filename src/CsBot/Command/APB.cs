using System;

namespace CsBot.Command
{
    class APB : ICommand
    {
	    readonly CommandHandler handler;

        public APB(CommandHandler handler)
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
                handler.Say($"{addresser}: Who do you want {ircBot.Settings.nick} to find?");
            }
            else
            {
                string toFind = command[(endCommand + 2)..].Trim();
                if (!users.HasUser(toFind))
                {
                    handler.Say($"{addresser}: That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine($"{addresser} put out apb for {toFind}");
                    handler.Say($"/me sends out the blood hounds to find {toFind}.");
                }
            }
        }
    }
}
