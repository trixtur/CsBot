using System;

namespace CsBot.Command
{
    class StringReplace : ICommand
    {
	    readonly CommandHandler handler;

        public StringReplace(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void Handle(string command, int endCommand, string verb)
        {
            if (verb != "s") return;

            var addresser = handler.GetAddresser();
            var ircBot = handler.GetIrcBot();
            var users = handler.GetUsers();
            var random = new Random();
            var fromChannel = handler.GetFromChannel();

            Console.WriteLine("Inside replace command.");
            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: What did you want {ircBot.Settings.Nick} to replace?");
            }
            else if (!command.Contains("/"))
            {
                handler.Say($"{addresser}: Usage is ~s/<wrong phrase>/<corrected phrase>/");
            }
            else
            {
                var toReplace = command.Substring(command.IndexOf("/") + 1, command.Substring(command.IndexOf("/") + 1).IndexOf("/"));
                var withString = command.Substring(command.IndexOf("/", command.IndexOf(toReplace))).Replace("/", "");
                var lastSaid = users.GetUserMessage(addresser);
                lastSaid = lastSaid.Replace(toReplace, withString);
                handler.Say($"{addresser} meant: {lastSaid}");
            }
        }
    }
}
