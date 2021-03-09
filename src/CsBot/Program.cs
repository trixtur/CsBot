namespace CsBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var ircBot = new IrcBot();
            ircBot.Start();
        }
    }
}
