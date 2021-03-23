using System;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class APB : ICommand
    {
	    readonly CommandHandler _handler;

		public string Name { get; }

        public APB(CommandHandler handler)
        {
            _handler = handler;
            Name = GetType ().Name;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != Name.ToLower()) return;

            var addresser = _handler.GetAddresser();
            var ircBot = _handler.GetIrcBot();
            var users = _handler.GetUsers();

            if (command.Length == endCommand + 1)
            {
                _handler.Say($"{addresser}: Who do you want {ircBot.Settings.Nick} to find?");
            }
            else
            {
                var toFind = command[(endCommand + 2)..].Trim();
                if (!users.HasUser(toFind))
                {
                    _handler.Say($"{addresser}: That person doesn't exist.");
                }
                else
                {
                    Console.WriteLine($"{addresser} put out apb for {toFind}");
                    _handler.Say($"/me sends out the blood hounds to find {toFind}.");
                }
            }
        }
    }
}
