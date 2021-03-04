using System.ServiceModel.Syndication;

namespace CsBot
{
    class User
    {
        int farkle_value;

        public string Name { get; set; }

        public string Message { get; set; }

        public int RPS { get; set; } = -2;

        public bool RPSFlag { get; set; }

        public int FarkleValue
        {
            get => farkle_value;
            set => farkle_value += value;
        }

        public bool FarkleFlag { get; set; }

        public bool HasFarkleToken { get; set; }

        public SyndicationFeed Feed { get; set; }

        public int FeedCount { get; set; }
    }
}