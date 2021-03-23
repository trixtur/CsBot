using System;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;

namespace CsBot.Command
{
	// FIXME, Why doesn't this implement ICommand
	class RssFeedCommand
    {
	    readonly CommandHandler _handler;
        SyndicationFeed _feed;
        int _count;

        Users Users { get => _handler.Users; }
        string Addresser { get => _handler.Addresser; }
        IrcBotService IrcBotService { get => _handler.IrcBotService; }

        public RssFeedCommand(CommandHandler handler)
        {
            _handler = handler;
        }

        public void GetNext()
        {
            var feed = Users[Addresser].Feed;
            var feedCount = Users[Addresser].FeedCount;
            if (feed == null)
            {
                _handler.Say($"You must use {IrcBotService.Settings.CommandStart}getfeeds before trying to list them.");
                return;
            }
            if (feedCount >= feed.Items.Count() || feedCount < 0)
            {
                feedCount = 0;
                _handler.Say("Reached max, starting over.");
            }

            for (; feedCount < feed.Items.Count(); feedCount++)
            {
                var item = feed.Items.ElementAt(feedCount);
                _handler.Say($"Item {feedCount + 1} of {feed.Items.Count()}: {item.Title.Text.Trim()}");
                _count++;
                if (_count == 4) break;
            }
            Users[Addresser].FeedCount = ++feedCount;
        }

        public void GetMore(string command, int endCommand)
        {
	        _feed = Users[Addresser].Feed;
            if (_feed == null)
            {
                _handler.Say($"You must use {IrcBotService.Settings.CommandStart}getfeeds before trying to get more information.");
                return;
            }

            if (command.Length == endCommand + 1 || !int.TryParse(command.Substring(endCommand + 2).Trim().ToLower(), out var feedNumber))
            {
                _handler.Say($"Usage: {IrcBotService.Settings.CommandStart}getmore # (Where # is an item from the rss feed)");
            }
            else
            {
                feedNumber--;
                if (feedNumber < 0 || feedNumber > _feed.Items.Count())
                {
                    _handler.Say($"Number is not in the right range. Must be from 1 to {_feed.Items.Count()}.");
                }
                else
                {
                    _count = 0;
                    _handler.Say($"Links for {_feed.Items.ElementAt(feedNumber).Title.Text}(Max 4):");
                    foreach (var link in _feed.Items.ElementAt(feedNumber).Links)
                    {
                        _handler.Say(link.Uri.ToString().Trim());
                        _count++;
                        if (_count == 4) return;
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
                    _handler.Say($"Usage: {IrcBotService.Settings.CommandStart}getfeeds http://<rsssite>/rss/<rssfeed#>");
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            try
            {
                _feed = SyndicationFeed.Load(rssReader);
                _handler.Users[Addresser].Feed = _feed;
                rssReader.Close();
                _count = 0;
                Users[Addresser].FeedCount = 4;

                _handler.Say($"Items 1 through 4 of {_feed.Items.Count()} items.");
                foreach (var item in _feed.Items)
                {
                    _handler.Say(item.Title.Text.Trim());
                    _count++;
                    if (_count == 4) return;
                }
            }
            catch (Exception e)
            {
                _handler.Say("Received invalid feed data.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
