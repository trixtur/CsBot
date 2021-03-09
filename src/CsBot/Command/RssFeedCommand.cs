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


        Users m_users => handler.m_users;
        string m_addresser => handler.m_addresser;
        IrcBot ircBot => handler.ircBot;

        public RssFeedCommand(CommandHandler handler)
        {
            this.handler = handler;
        }

        public void GetNext()
        {
            var feed = m_users[m_addresser].Feed;
            var feedCount = m_users[m_addresser].FeedCount;
            if (feed == null)
            {
                handler.Say($"You must use {ircBot.Settings.command_start}getfeeds before trying to list them.");
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
            m_users[m_addresser].FeedCount = ++feedCount;
        }

        public void GetMore(string command, int endCommand)
        {
	        feed = m_users[m_addresser].Feed;
            if (feed == null)
            {
                handler.Say($"You must use {ircBot.Settings.command_start}getfeeds before trying to get more information.");
                return;
            }

            if (command.Length == endCommand + 1 || !int.TryParse(command.Substring(endCommand + 2).Trim().ToLower(), out var feedNumber))
            {
                handler.Say($"Usage: {ircBot.Settings.command_start}getmore # (Where # is an item from the rss feed)");
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
                    handler.Say($"Usage: {ircBot.Settings.command_start}getfeeds http://<rsssite>/rss/<rssfeed#>");
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            try
            {
                feed = SyndicationFeed.Load(rssReader);
                handler.m_users[m_addresser].Feed = feed;
                rssReader.Close();
                count = 0;
                m_users[m_addresser].FeedCount = 4;

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
