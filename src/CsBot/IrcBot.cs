using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CsBot
{
    /// <summary>
    /// This program establishes a connection to an irc server, joins
    /// a channel and greets every nickname that joins the channel.
    /// </summary>
    class IrcBot
    {
        //private static string SETTINGS_FILE = "settings.json";
        readonly string SETTINGS_FILE = ConfigurationManager.AppSettings["Site"];
        public Settings Settings { get; private set; }

        // StreamWriter is declared here so that PingSender can access it
        public StreamWriter Writer { get; private set; }
        public StreamReader Reader { get; private set; }
        CommandHandler commandHandler;
        TcpClient m_irc;
        static bool isUnderscoreNick;

        public IrcBot()
        {
            commandHandler = new CommandHandler(this);
        }

        public void Start ()
        {
            string nickname = "";
            string addresser = "";

            try
            {
                ObtainConfig();

                var fromChannel = Settings.channels[0].Name;
                using (m_irc = new TcpClient())
                {

                    Console.WriteLine($"Trying to connect to server {Settings.server}.");
                    m_irc.Connect(Settings.server, Settings.port);

                    Stream stream;
                    if (Settings.secure == "1")
                    {
                        stream = new SslStream(m_irc.GetStream(), true, ValidateServerCertificate);
                        ((SslStream) stream).AuthenticateAsClient(Settings.server);
                    }
                    else
                    {
                        stream = m_irc.GetStream();
                    }

                    Reader = new StreamReader(stream);
                    Writer = new StreamWriter(stream);
                    Console.WriteLine(Settings.user);
                    // Start PingSender thread
                    var ping = new PingSender(this);
                    ping.Start();
                    Writer.WriteLine(Settings.user);
                    Writer.Flush();
                    Writer.WriteLine($"NICK {Settings.nick}");
                    Writer.Flush();
                    Writer.WriteLine($"PASS {Settings.password}");
                    Writer.Flush();
                    Console.WriteLine($"NICK {Settings.nick}");
                    commandHandler = new CommandHandler(this);
                    Writer.WriteLine($"PRIVMSG mattermost LOGIN {Settings.nick} {Settings.password}");
                    Writer.Flush();
                    //Writer.WriteLine("JOIN " + settings.channels[0].Name + " " + KEY);
                    //Writer.WriteLine("JOIN " + settings.channels[0].Name);
                    //Writer.Flush();
                    //Writer.WriteLine("JOIN " + settings.channels[0].name2);
                    //Writer.Flush();

                    EventLoop(addresser, fromChannel, ping, nickname, stream);
                }
            }
            catch (Exception e)
            {
                // Show the exception, sleep for a while and try to establish a new connection to irc server
                Console.WriteLine($"Exception info: {e}");
                Task.Delay(5000);

                commandHandler.HandleMessage($":{Settings.command_start}say Awe, Crap!", "#bots", "self");

                var argv = Array.Empty<string>();
                Start();
            }
            finally
            {
                // Close all streams
                Writer?.Close();
                Reader?.Close();
                m_irc?.Close();
            }
        }

        void CloseProgram()
        {
            // Close all streams
            Writer.Close();
            Reader.Close();
            m_irc.Close();
            Environment.Exit(0);
        }

        void EventLoop(string addresser, string fromChannel, PingSender ping, string nickname, object stream)
        {
            bool joined = false;
            bool identified = true;

            while (true)
            {
                var inputLine = Reader.ReadLine();
                string parsedLine = null;

                foreach (var channel in Settings.channels)
                {
                    if (inputLine.Contains(channel.Name) && inputLine.IndexOf("#") > -1)
                        fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];

                    if (inputLine.Contains($"{Settings.nick} = {channel.Name}") || inputLine.Contains(
		                    $"{Settings.nick} = {channel.Name}"))
                    //if (inputLine.Contains(settings.nick + " = " + settings.channels[0].Name))
                        commandHandler.ParseUsers(inputLine);

                    //if (inputLine.Contains(settings.channels[0].Name))
                    if (joined && !inputLine.EndsWith(fromChannel))
                    {
                        //parsedLine = inputLine.Substring(inputLine.IndexOf(m_fromChannel) + m_fromChannel.Length + 1);
                        if (!inputLine.EndsWith(channel.Name) && (parsedLine == null || !parsedLine.StartsWith(
	                        $":{Settings.command_start}")) && channel.Name == fromChannel)
                            parsedLine = inputLine.Substring(inputLine.IndexOf(fromChannel) + channel.Name.Length + 1).Trim();
                    }
                }

                if (!joined)
                    Console.WriteLine(inputLine);

                if (inputLine.Contains(Constants.NICK))
                {
                    string origUser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                    string newUser = inputLine.Substring(inputLine.IndexOf(Constants.NICK) + 6);
                    commandHandler.UpdateUserName(origUser, newUser);
                }

                if (inputLine.EndsWith($"JOIN {fromChannel}"))
                {
                    // Parse nickname of person who joined the channel
                    nickname = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                    if (nickname == Settings.nick)
                    {
                        if (fromChannel == Settings.channels[0].Name)
                            joined = true;

                        if (fromChannel == "#bots") {
                            commandHandler.HandleMessage($":{Settings.command_start}say I'm back baby!", fromChannel, addresser);
                        }
                        continue;
                    }
                    // Welcome the nickname to channel by sending a notice
                    Writer.WriteLine($"NOTICE {nickname}: Hi {nickname} and welcome to {fromChannel} channel!");
                    commandHandler.HandleMessage(
	                    $":{Settings.command_start}say {nickname}: Hi and welcome to {fromChannel} channel!", fromChannel, addresser);
                    commandHandler.AddUser(nickname);
                    Writer.Flush();
                    // Sleep to prevent excess flood
                    Task.Delay(2000);
                }
                else if (inputLine.StartsWith(":NickServ") && inputLine.Contains("You are now identified"))
                {
                    identified = true;
                    Console.WriteLine(inputLine);
                }
                else if (inputLine.Contains("!") && inputLine.Contains($" :{Settings.command_start}quit"))
                {
                    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                    bool useChannel = false;
                    if (inputLine.Contains("#"))
                    {
                        useChannel = true;
                        fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
                    }

                    if (Settings.admins != null && Array.IndexOf(Settings.admins, addresser) >= 0)
                    {
                        commandHandler.HandleMessage($":{Settings.command_start}say Awe, Crap!", "#bots", addresser);

                        ping.Stop();
                        CloseProgram();
                    }
                    else
                    {
                        commandHandler.Say("You don't have permissions.", useChannel ? fromChannel : addresser);
                    }
                }
                else if (inputLine.Contains("!") && inputLine.Contains($" :{Settings.command_start}reload"))
                {
                    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                    bool useChannel = false;
                    if (inputLine.Contains("#"))
                    {
                        useChannel = true;
                        fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
                    }

                    if (Settings.admins != null && Array.IndexOf(Settings.admins, addresser) >= 0)
                    {
                        ObtainConfig();
                        commandHandler.Say("Reloaded settings from web service.", useChannel ? fromChannel : addresser);
                    }
                    else
                    {
                        commandHandler.Say("You don't have permissions.", useChannel ? fromChannel : addresser);
                    }
                }
                else if (inputLine.Contains(Settings.command_start) && parsedLine != null && parsedLine.StartsWith(
	                $":{Settings.command_start}"))
                {
                    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                    fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
                    commandHandler.HandleMessage(parsedLine, fromChannel, addresser);
                }
                else if (inputLine.StartsWith(Constants.PING))
                {
                    Writer.WriteLine(Constants.PONG + inputLine.Substring(inputLine.IndexOf(":") + 1));
                    Writer.Flush();
                }
                else if (inputLine.Contains("PONG") && (!joined))
                {
                    if (isUnderscoreNick)
                    {
                        Writer.WriteLine($"PRIVMSG NickServ :ghost {Settings.nick} {Settings.password}");
                        Writer.WriteLine($"NICK {Settings.nick}");
                        Console.WriteLine($"NICK {Settings.nick}");
                        commandHandler.HandleMessage($":{Settings.command_start}say identify {Settings.password}", "NickServ", Settings.nick);
                        isUnderscoreNick = false;
                    }
                    else
                    {
                        foreach (var channel in Settings.channels)
                        {
                            if (channel.Key != "")
                                Writer.WriteLine($"JOIN {channel.Name} {channel.Key}");
                            else
                                Writer.WriteLine($"JOIN {channel.Name}");

                            Writer.Flush();
                        }
                    }
                }
                else if (inputLine.Contains("PONG") && (joined) && !identified)
                {
                    commandHandler.HandleMessage($":{Settings.command_start}say identify {Settings.password}", "NickServ", addresser);
                }
                else if (inputLine.Contains("LOGIN"))
                {
                    Console.WriteLine($"{Settings.nick} and {Settings.password}");
                    Writer.WriteLine($"PRIVMSG mattermost LOGIN {Settings.nick} {Settings.password}");
                    Writer.Flush();
                }
                else if (inputLine.Contains(":Nickname is already in use."))
                {
                    Console.WriteLine("Reopening with _ nick.");

                    Writer.Close();
                    Reader.Close();
                    m_irc.Close();
                    m_irc = new TcpClient();
                    m_irc.Connect(Settings.server, Settings.port);
                    if (Settings.secure == "1")
                    {
                        stream = new SslStream(m_irc.GetStream(), true, ValidateServerCertificate);
                        SslStream sslStream = (SslStream)stream;
                        sslStream.AuthenticateAsClient(Settings.server);
                    }
                    else
                    {
                        stream = m_irc.GetStream();
                    }

                    Reader = new StreamReader((Stream)stream);
                    Writer = new StreamWriter((Stream)stream);
                    Console.WriteLine(Settings.user);
                    // Start PingSender thread
                    ping = new PingSender(this);
                    ping.Start();
                    Writer.WriteLine(Settings.user);
                    Writer.Flush();
                    Writer.WriteLine($"NICK _{Settings.nick}");
                    Console.WriteLine($"NICK _{Settings.nick}");
                    commandHandler = new CommandHandler(this);
                    isUnderscoreNick = true;
                }
                else if (inputLine.Contains("PRIVMSG") && (inputLine.Contains("rock") || inputLine.Contains("paper") || inputLine.Contains("scissors")))
                {
                    Console.WriteLine(inputLine);
                    addresser = inputLine.Substring(inputLine.IndexOf(":") + 1, inputLine.IndexOf("!") - inputLine.IndexOf(":") - 1);
                    string choice = inputLine.Substring(inputLine.LastIndexOf(":") + 1);
                    commandHandler.DirectRoShamBo(choice);

                }
                else if (inputLine.Contains("PRIVMSG") && inputLine.Contains($":{Settings.command_start}"))
                {
                    Console.WriteLine($"PrivateMessage: {inputLine}");
                    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                    string command = inputLine.Substring(inputLine.LastIndexOf($":{Settings.command_start}"));
                    commandHandler.HandleMessage(command, addresser, addresser);
                }
                else
                {
                    if (inputLine.Contains("PRIVMSG") && inputLine.Contains("!"))
                    {
                        string userName = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
                        commandHandler.LastMessage(userName, inputLine, fromChannel);
                    }
                }
            }
        }

        void ObtainConfig()
        {
            Console.WriteLine("Trying to pull config.");
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            using (var webClient = new WebClient())
            {
                Console.WriteLine("Pulling config.");
                Console.WriteLine($"Settings file: {SETTINGS_FILE}");
                var setting_file = webClient.DownloadString(SETTINGS_FILE);
                Settings = JsonConvert.DeserializeObject<Settings>(setting_file);
                Settings.password = ConfigurationManager.AppSettings["Password"];
                Console.WriteLine($"Config File:{Settings}");
            }
        }

        bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // TODO take this out for actual validation.
            return true;
            foreach (X509ChainStatus status in chain.ChainStatus)
            {
                if (certificate.Subject == certificate.Issuer &&
                        status.Status == X509ChainStatusFlags.UntrustedRoot &&
                        Settings != null && Settings.server_validate == false)
                {
                    continue;

                }
                else if (status.Status != X509ChainStatusFlags.NoError &&
                        ((Settings != null && Settings.server_validate != false) || status.Status != X509ChainStatusFlags.UntrustedRoot))
                {
                    Console.WriteLine("Certificate not valid: {0}, {1}", sslPolicyErrors, status.StatusInformation);
                    return false;
                }
            }

            return true;
        }
    }
}
