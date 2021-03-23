using System;

using CsBot.Interfaces;

namespace CsBot.Command
{
    class StringReplace : ICommand
    {
	    readonly CommandHandler _handler;

	    public string Name { get; }

	    public StringReplace (CommandHandler handler)
        {
            _handler = handler;

            Name = GetType ().Name;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != "s") return;

            var addresser = _handler.GetAddresser();
            var ircBot = _handler.GetIrcBot();
            var users = _handler.GetUsers();
            var random = new Random();
            var fromChannel = _handler.GetFromChannel();

            Console.WriteLine("Inside replace command.");
            if (command.Length == endCommand + 1)
            {
                _handler.Say($"{addresser}: What did you want {ircBot.Settings.Nick} to replace?");
            }
            else if (!command.Contains("/"))
            {
                _handler.Say($"{addresser}: Usage is ~s/<wrong phrase>/<corrected phrase>/");
            }
            else
            {
                var toReplace = command.Substring(command.IndexOf("/") + 1, command.Substring(command.IndexOf("/") + 1).IndexOf("/"));
                var withString = command.Substring(command.IndexOf("/", command.IndexOf(toReplace))).Replace("/", "");
                var lastSaid = users.GetUserMessage(addresser);
                lastSaid = lastSaid.Replace(toReplace, withString);
                _handler.Say($"{addresser} meant: {lastSaid}");
            }
        }
    }
}
