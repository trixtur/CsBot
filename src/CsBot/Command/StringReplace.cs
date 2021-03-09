using System;

namespace CsBot.Command
{
    class StringReplace : iCommand
    {
        CommandHandler handler;

        public StringReplace(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void handle(string command, int endCommand, string verb)
        {
            if (verb != "s") return;

            string addresser = this.handler.GetAddresser();
            IrcBot ircBot = this.handler.GetIrcBot();
            Users users = this.handler.GetUsers();
            Random random = new Random();
            string fromChannel = this.handler.GetFromChannel();

            Console.WriteLine("INside replace command.");
            if (command.Length == endCommand + 1)
            {
                this.handler.Say(addresser + ": What did you want " + ircBot.Settings.nick + " to replace?");
            }
            else if (command.IndexOf("/") == -1)
            {
                this.handler.Say(addresser + ": Usage is ~s/<wrong phrase>/<corrected phrase>/");
            }
            else
            {
                string toReplace = command.Substring(command.IndexOf("/") + 1, command.Substring(command.IndexOf("/") + 1).IndexOf("/"));
                string withString = command.Substring(command.IndexOf("/", command.IndexOf(toReplace))).Replace("/", "");
                string lastSaid = users.getUserMessage(addresser);
                lastSaid = lastSaid.Replace(toReplace, withString);
                this.handler.Say(addresser + " meant: " + lastSaid);
            }
        }
    }
}
