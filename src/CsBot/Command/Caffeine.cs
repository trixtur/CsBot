using System;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class Caffeine : ICommand
    {
	    readonly CommandHandler _handler;

	    public string Name { get; }


		public Caffeine (CommandHandler handler)
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
            var random = new Random();

            if (command.Length == endCommand + 1)
            {
                _handler.Say($"/me walks over to {addresser} and gives them a shot of caffeine straight into the blood stream.");
            }
            else
            {
	            if (!int.TryParse(command.Substring(endCommand + 2).Trim(), out var shots))
                {
                    _handler.Say($"{addresser}: I didn't understand, how many shots of caffeine did you want?");
                }
                else if (shots == 1)
                {
                    _handler.Say($"/me walks over to {addresser} and gives them a shot of caffeine straight into the blood stream.");
                }
                else
                {
                    _handler.Say($"/me walks over to {addresser} and gives them {shots} shots of caffeine straight into the blood stream.");
                }
            }
        }
    }
}
