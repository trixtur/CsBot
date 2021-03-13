using System;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;

namespace CsBot.Command
{
	class RssFeedCommand
    {
	    readonly CommandHandler handler;
        SyndicationFeed feed;
        int count;


        Users Users { get => handler.Users; }
        string Addresser { get => handler.Addresser; }
        IrcBot IrcBot { get => handler.IrcBot; }

        public RssFeedCommand(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void GetNext()
        {
            var feed = Users[Addresser].Feed;
            var feedCount = Users[Addresser].FeedCount;
            if (feed == null)
            {
                handler.Say($"You must use {IrcBot.Settings.CommandStart}getfeeds before trying to list them.");
                return;
            }
            if (feedCount >= feed.Items.Count() || feedCount < 0)
            {
                feedCount = 0;
                handler.Say("Reached max, starting over.");
            }

            for (; feedCount < feed.Items.Count(); feedCount++)
            {
                var item = feed.Items.ElementAt(feedCount);
                handler.Say($"Item {feedCount + 1} of {feed.Items.Count()}: {item.Title.Text.Trim()}");
                count++;
                if (count == 4) break;
            }
            Users[Addresser].FeedCount = ++feedCount;
        }

        public void GetMore(string command, int endCommand)
        {
	        feed = Users[Addresser].Feed;
            if (feed == null)
            {
                handler.Say($"You must use {IrcBot.Settings.CommandStart}getfeeds before trying to get more information.");
                return;
            }

            if (command.Length == endCommand + 1 || !int.TryParse(command.Substring(endCommand + 2).Trim().ToLower(), out var feedNumber))
            {
                handler.Say($"Usage: {IrcBot.Settings.CommandStart}getmore # (Where # is an item from the rss feed)");
            }
            else
            {
                feedNumber--;
                if (feedNumber < 0 || feedNumber > feed.Items.Count())
                {
                    handler.Say($"Number is not in the right range. Must be from 1 to {feed.Items.Count()}.");
                }
                else
                {
                    count = 0;
                    handler.Say($"Links for {feed.Items.ElementAt(feedNumber).Title.Text}(Max 4):");
                    foreach (var link in feed.Items.ElementAt(feedNumber).Links)
                    {
                        handler.Say(link.Uri.ToString().Trim());
                        count++;
                        if (count == 4) return;
                    }
                }
            }
        }

        public void GetFeeds(string command, int endCommand)
        {
            XmlReader rssReader;
            if (command.Length == endCommand + 1)
            {
                rssReader = XmlReader.Create("http://rss.news.yahoo.com/rss/topstories#");
            }
            else
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(command.Substring(endCommand + 2).Trim().ToLower());
                    request.AllowAutoRedirect = true;
                    var response = (HttpWebResponse)request.GetResponse();
                    rssReader = XmlReader.Create(response.GetResponseStream());
                }
                catch (Exception e)
                {
                    handler.Say($"Usage: {IrcBot.Settings.CommandStart}getfeeds http://<rsssite>/rss/<rssfeed#>");
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            try
            {
                feed = SyndicationFeed.Load(rssReader);
                handler.Users[Addresser].Feed = feed;
                rssReader.Close();
                count = 0;
                Users[Addresser].FeedCount = 4;

                handler.Say($"Items 1 through 4 of {feed.Items.Count()} items.");
                foreach (var item in feed.Items)
                {
                    handler.Say(item.Title.Text.Trim());
                    count++;
                    if (count == 4) return;
                }
            }
            catch (Exception e)
            {
                handler.Say("Received invalid feed data.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
