namespace IrcBot.cs
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.IO;
    using System.Threading;
    using System.Collections.Generic;
    using CsBot;
    
    /*
    * This program establishes a connection to irc server, joins a channel and greets every nickname that
    * joins the channel.
    *an
    */
    class IrcBot
    {
        // Irc server to connect
        //public static string SERVER = "10.87.183.29";
        public static string SERVER = "kr0w.com"; //"chat.freenode.net";
        public static string PASSWORD = "m1b0t";
        // Irc server's port (6667 is default port)
        private static int PORT = 6667;
        // User information defined in RFC 2812 (Internet Relay Chat: Client Protocol) is sent to irc server
        private static string USER = "USER be|\\|der 198.91.54.142 kr0w :Be|\\|der";
        // Bot's nickname
        private static string NICK = "Be|\\|der";
        // Channel to join
        private static string CHANNEL = "#pyrous";
        private static string KEY = ""; //Example: jeff
        private static string CHANNEL2 = "#CsBot";
        // StreamWriter is declared here so that PingSender can access it
        public static StreamWriter writer;
        public static StreamReader reader;
        private static CommandHandler ch;
        private static TcpClient m_irc;

        static void Main(string[] args)
        {
            NetworkStream stream;
            string inputLine;
            string nickname;
            string fromChannel = CHANNEL;
            string addresser = "";
            try
            {
                m_irc = new TcpClient();
                bool joined1 = false;
                bool joined2 = false;
                bool identified = false;
                m_irc.Connect(SERVER, PORT);
                stream = m_irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                Console.WriteLine(USER);
                // Start PingSender thread
                PingSender.cs.PingSender ping = new PingSender.cs.PingSender();
                ping.Start();
                writer.WriteLine(USER);
                writer.Flush();
                writer.WriteLine("NICK " + NICK);
                writer.Flush();
                Console.WriteLine("NICK " + NICK);
                ch = new CommandHandler(writer, reader);
                //writer.WriteLine("JOIN " + CHANNEL + " " + KEY);
                //writer.WriteLine("JOIN " + CHANNEL);
                //writer.Flush();
                //writer.WriteLine("JOIN " + CHANNEL2);
                //writer.Flush();
                while (true)
                {
                    while ((inputLine = reader.ReadLine()) != null)
                    {
                        if( inputLine.Contains(CHANNEL) || inputLine.Contains(CHANNEL2))
                        //if (inputLine.Contains(CHANNEL))
                            fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
                        string parsedLine = null;
                        if (inputLine.Contains(NICK + " = " + CHANNEL) || inputLine.Contains(NICK + " = " + CHANNEL2))
                        //if (inputLine.Contains(NICK + " = " + CHANNEL))
                        {
                            CsBot.CommandHandler.ParseUsers(inputLine);
                        }
                        if (joined1 && !inputLine.EndsWith(fromChannel))
                        {
                            //parsedLine = inputLine.Substring(inputLine.IndexOf(m_fromChannel) + m_fromChannel.Length + 1);
                            if (!inputLine.EndsWith(CHANNEL) && (parsedLine == null || !parsedLine.StartsWith(":~")))
                            {
                                parsedLine = inputLine.Substring(inputLine.IndexOf(fromChannel) + CHANNEL.Length + 1).Trim();
                            }
                        }
                        if (joined2 && !inputLine.EndsWith(fromChannel))
                        {
                            //parsedLine = inputLine.Substring(inputLine.IndexOf(m_fromChannel) + m_fromChannel.Length + 1);
                            if (!inputLine.EndsWith(CHANNEL2) && (parsedLine == null || !parsedLine.StartsWith(":~")))
                            {
                                parsedLine = inputLine.Substring(inputLine.IndexOf(fromChannel) + CHANNEL2.Length + 1).Trim();
                            }
                        }

                        if (!joined1 || !joined2)
                        {
                            Console.WriteLine(inputLine);
                        }

                        if (inputLine.EndsWith("JOIN " + fromChannel))
                        {
                            // Parse nickname of person who joined the channel
                            nickname = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                            if (nickname == NICK)
                            {
                                if (fromChannel == CHANNEL)
                                    joined1 = true;
                                else if (fromChannel == CHANNEL2)
                                    joined2 = true;
                                ch.HandleMessage(":~say Hello All!", fromChannel, addresser);
                                continue;
                            }
                            // Welcome the nickname to channel by sending a notice
                            writer.WriteLine("NOTICE " + nickname + ": Hi " + nickname +
                            " and welcome to " + fromChannel + " channel!");
                            ch.HandleMessage(":~say " + nickname + ": Hi and welcome to " + fromChannel + " channel!", fromChannel, addresser);
                            ch.AddUser(nickname);
                            writer.Flush();
                            // Sleep to prevent excess flood
                            Thread.Sleep(2000);
                        }
                        else if (inputLine.StartsWith(":NickServ") && inputLine.Contains("You are now identified"))
                        {
                            identified = true;
                            Console.WriteLine(inputLine);
                        }
                        else if (inputLine.Contains("~quit"))
                        {
                            ping.Stop();
                            goto CloseProgram;
                        }
                        else if (inputLine.Contains("~") && parsedLine != null && parsedLine.StartsWith(":~"))
                        {
                            addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                            fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
                            ch.HandleMessage(parsedLine, fromChannel, addresser);
                        }
                        else if (inputLine.StartsWith("PING :"))
                        {
                            writer.WriteLine("PONG :" + inputLine.Substring(inputLine.IndexOf(":") + 1));
                            writer.Flush();
                        }
                        else if (inputLine.Contains("PONG") && (!joined1 || !joined2))
                        {
                            if (KEY != "")
                                writer.WriteLine("JOIN " + CHANNEL + " " + KEY);
                            else
                            {
                                writer.WriteLine("JOIN " + CHANNEL);
                            }
                            writer.Flush();
                            writer.WriteLine("JOIN " + CHANNEL2);
                            writer.Flush();
                        }
                        else if (inputLine.Contains("PONG") && (joined1 || joined2) && !identified)
                        {
                            ch.HandleMessage(":~say identify " + PASSWORD, "NickServ", addresser);
                        }
                        else if (inputLine.Contains(NICK) && inputLine.Contains("PRIVMSG") && (inputLine.Contains("rock") || inputLine.Contains("paper") || inputLine.Contains("scissors")))
                        {
                            addresser = inputLine.Substring(inputLine.IndexOf(":") + 1, inputLine.IndexOf("!") - inputLine.IndexOf(":") - 1);
                            string choice = inputLine.Substring(inputLine.LastIndexOf(":") + 1);
                            ch.DirectRoShamBo(choice);

                        }
                        else if (inputLine.Contains(NICK) && inputLine.Contains("PRIVMSG") && inputLine.Contains(":~"))
                        {
                            addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                            string command = inputLine.Substring(inputLine.LastIndexOf(":"));
                            ch.HandleMessage(command, addresser, addresser);
                        }
                        else
                        {
                            if (inputLine.Contains("PRIVMSG") && inputLine.Contains("!"))
                            {
                                string userName = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                                ch.LastMessage(userName, inputLine);
                            }
                        }
                    }
                }
                CloseProgram:
                // Close all streams
                writer.Close();
                reader.Close();
                m_irc.Close();
                return;
            }
            catch (Exception e)
            {
                // Close all streams
                writer.Close();
                reader.Close();
                m_irc.Close();
                // Show the exception, sleep for a while and try to establish a new connection to irc server
                Console.WriteLine("Exception info: " + e.ToString());
                Thread.Sleep(5000);
                string[] argv = { };
                Main(argv);
            }
        }
    }
}

namespace PingSender.cs
{
    using System;
    using System.Threading;
    /*
    * Class that sends PING to irc server every 15 seconds
    */
    class PingSender
    {
        static string PING = "PING :";
        private Thread pingSender;
        // Empty constructor makes instance of Thread
        public PingSender()
        {
            pingSender = new Thread(new ThreadStart(this.Run));
        }
        // Starts the thread
        public void Start()
        {
            pingSender.Start();
        }
        // Kills the thead
        public void Stop()
        {
            pingSender.Abort();
        }
        // Send PING to irc server every 15 seconds
        public void Run()
        {
            while (true)
            {
                IrcBot.cs.IrcBot.writer.WriteLine(PING + IrcBot.cs.IrcBot.SERVER);
                IrcBot.cs.IrcBot.writer.Flush();
                Thread.Sleep(15000);
            }
        }
    }
}
