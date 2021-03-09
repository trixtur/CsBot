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

            string addresser = handler.GetAddresser();
            IrcBot ircBot = handler.GetIrcBot();
            Users users = handler.GetUsers();
            Random random = new Random();
            string fromChannel = handler.GetFromChannel();

            Console.WriteLine("Inside replace command.");
            if (command.Length == endCommand + 1)
            {
                handler.Say($"{addresser}: What did you want {ircBot.Settings.nick} to replace?");
            }
            else if (!command.Contains("/"))
            {
                handler.Say($"{addresser}: Usage is ~s/<wrong phrase>/<corrected phrase>/");
            }
            else
            {
                string toReplace = command.Substring(command.IndexOf("/") + 1, command.Substring(command.IndexOf("/") + 1).IndexOf("/"));
                string withString = command.Substring(command.IndexOf("/", command.IndexOf(toReplace))).Replace("/", "");
                string lastSaid = users.GetUserMessage(addresser);
                lastSaid = lastSaid.Replace(toReplace, withString);
                handler.Say($"{addresser} meant: {lastSaid}");
            }
        }
    }
}
