
using System.Timers;

namespace CsBot
{
    /// <summary>
    /// Class that sends PING to irc server every 15 seconds
    /// </summary>
    class PingSender
    {
        readonly Timer timer;
        readonly IrcBot ircBot;

        // Empty constructor makes instance of Thread
        public PingSender(IrcBot ircBot)
        {
            this.ircBot = ircBot;

            timer = new Timer {
                Interval = 15000
            };
        }

        // Send PING to irc server every 15 seconds
        void SendPing(object sender, ElapsedEventArgs e)
        {
            ircBot.Writer.WriteLine(Constants.PING + ircBot.Settings.server);
            ircBot.Writer.Flush();
        }

        // Starts the thread
        public void Start()
        {
            timer.Enabled = true;
            timer.Elapsed += SendPing;
        }

        // Kills the thead
        public void Stop()
        {
            timer.Elapsed -= SendPing;
            timer.Enabled = false;
        }
    }
}