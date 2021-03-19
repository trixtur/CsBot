
using System.Timers;

namespace CsBot
{
    /// <summary>
    /// Class that sends PING to irc server every 15 seconds
    /// </summary>
    class PingSender
    {
        readonly Timer _timer;
        readonly IrcBotService _ircBotService;

        // Empty constructor makes instance of Thread
        public PingSender(IrcBotService ircBotService)
        {
            _ircBotService = ircBotService;

            _timer = new Timer {
                Interval = 15000
            };
        }

        // Send PING to irc server every 15 seconds
        void SendPing(object sender, ElapsedEventArgs e)
        {
            _ircBotService.Writer.WriteLine(Constants.PING + _ircBotService.Settings.Server);
            _ircBotService.Writer.Flush();
        }

        // Starts the thread
        public void Start()
        {
            _timer.Enabled = true;
            _timer.Elapsed += SendPing;
        }

        // Kills the thead
        public void Stop()
        {
            _timer.Elapsed -= SendPing;
            _timer.Enabled = false;
        }
    }
}
