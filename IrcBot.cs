namespace IrcBot.cs
{
    using System;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Net.Sockets;
    using System.IO;
    using System.Threading;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Configuration;
    using CsBot;
    
    /*
    * This program establishes a connection to irc server, joins a channel and greets every nickname that
    * joins the channel.
    *an
    */
    class IrcBot
    {
        //private static string SETTINGS_FILE = "settings.json";
        private static string SETTINGS_FILE = ConfigurationManager.AppSettings["Site"];
        public static Settings settings;
        // StreamWriter is declared here so that PingSender can access it
        public static StreamWriter writer;
        public static StreamReader reader;
        private static CommandHandler ch;
        private static TcpClient m_irc;
        private static bool isUnderscoreNick = false;

        static void Main(string[] args)
        {
            object stream;
            string inputLine = "";
            string nickname = "";
            string addresser = "";
	    string fromChannel;
            try
            {
                //FileStream setting_file = new FileStream(SETTINGS_FILE, FileMode.Open);

		settings = IrcBot.ObtainConfig();

                fromChannel = settings.channels[0].name;
                m_irc = new TcpClient();
                bool joined1 = false;
                bool joined2 = false;
                bool identified = true;
                m_irc.Connect(settings.server, settings.port);
                if (settings.secure == "1") {
                    stream = new SslStream (m_irc.GetStream(), true, new RemoteCertificateValidationCallback (ValidateServerCertificate));
                    SslStream sslStream = (SslStream) stream;
                    sslStream.AuthenticateAsClient(settings.server);
                } else {
                    stream = m_irc.GetStream();
                }
                reader = new StreamReader((Stream)stream);
                writer = new StreamWriter((Stream)stream);
                Console.WriteLine(settings.user);
                // Start PingSender thread
                PingSender.cs.PingSender ping = new PingSender.cs.PingSender();
                ping.Start();
                writer.WriteLine(settings.user);
                writer.Flush();
                writer.WriteLine("NICK " + settings.nick);
                writer.Flush();
                writer.WriteLine("PASS " + settings.password);
                writer.Flush();
                Console.WriteLine("NICK " + settings.nick);
                ch = new CommandHandler(writer, reader);
                CsBot.CommandHandler.settings = settings;
                //writer.WriteLine("JOIN " + settings.channels[0].name + " " + KEY);
                //writer.WriteLine("JOIN " + settings.channels[0].name);
                //writer.Flush();
                //writer.WriteLine("JOIN " + settings.channels[0].name2);
                //writer.Flush();

		IrcBot.EventLoop(inputLine, 
			addresser, 
			fromChannel, 
			ping, 
			joined1, 
			joined2,
			nickname,
			identified,
			stream);

            }
            catch (Exception e)
            {
		foreach (Channel channel in settings.channels) {
			ch.HandleMessage(":" + settings.command_start + "say Awe, Crap!", channel.name, "self");
		}

                // Close all streams
                if(writer != null)
                writer.Close();
                if(reader != null)
                reader.Close();
		if (m_irc != null) {
			m_irc.Close();
		}
                // Show the exception, sleep for a while and try to establish a new connection to irc server
                Console.WriteLine("Exception info: " + e.ToString());
                Thread.Sleep(5000);
                string[] argv = { };
                Main(argv);
            }
        }

	private static void CloseProgram(StreamWriter writer,StreamReader reader,TcpClient m_irc) 
	{
                // Close all streams
                writer.Close();
                reader.Close();
                m_irc.Close();
		System.Environment.Exit(0);
	}

	private static void EventLoop(
		string inputLine, 
		string addresser, 
		string fromChannel,
		PingSender.cs.PingSender ping,
		bool joined1,
		bool joined2,
		string nickname,
		bool identified,
		object stream) 
	{
                
	    while (true)
	    {
		inputLine = reader.ReadLine();
		string parsedLine = null;
		foreach (var channel in settings.channels) {
			if (inputLine.Contains(channel.name)) {
				fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
			}

			if (inputLine.Contains(settings.nick + " = " + channel.name) || inputLine.Contains(settings.nick + " = " + channel.name))
			//if (inputLine.Contains(settings.nick + " = " + settings.channels[0].name))
			{
			    CsBot.CommandHandler.ParseUsers(inputLine);
			}
			//if (inputLine.Contains(settings.channels[0].name))
			if (joined1 && !inputLine.EndsWith(fromChannel))
			{
			    //parsedLine = inputLine.Substring(inputLine.IndexOf(m_fromChannel) + m_fromChannel.Length + 1);
			    if (!inputLine.EndsWith(channel.name) && (parsedLine == null || !parsedLine.StartsWith(":" + settings.command_start)))
			    {
				parsedLine = inputLine.Substring(inputLine.IndexOf(fromChannel) + channel.name.Length + 1).Trim();
			    }
			}
		}

		if (!joined1 || !joined2)
		{
		    Console.WriteLine(inputLine);
		}

		if (inputLine.Contains("NICK :")) {
		    string origUser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
		    string newUser = inputLine.Substring(inputLine.IndexOf("NICK :") + 6);
		    CsBot.CommandHandler.UpdateUserName(origUser, newUser);
		}
		if (inputLine.EndsWith("JOIN " + fromChannel))
		{
		    // Parse nickname of person who joined the channel
		    nickname = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
		    if (nickname == settings.nick)
		    {
			if (fromChannel == settings.channels[0].name)
			    joined1 = true;
			else if (fromChannel == settings.channels[1].name)
			    joined2 = true;
			ch.HandleMessage(":" + settings.command_start + "say I'm back baby!", fromChannel, addresser);
			continue;
		    }
		    // Welcome the nickname to channel by sending a notice
		    writer.WriteLine("NOTICE " + nickname + ": Hi " + nickname +
		    " and welcome to " + fromChannel + " channel!");
		    ch.HandleMessage(":" + settings.command_start + "say " + nickname + ": Hi and welcome to " + fromChannel + " channel!", fromChannel, addresser);
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
		else if (inputLine.Contains("!") && inputLine.Contains(" :" + settings.command_start + "quit"))
		{
		    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
		    bool useChannel = false;
		    if (inputLine.IndexOf("#") >= 0) {
			useChannel = true;
			fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
		    }

		    if (settings.admins != null && Array.IndexOf(settings.admins, addresser) >= 0) {
			foreach (Channel channel in settings.channels) {
				ch.HandleMessage(":" + settings.command_start + "say Awe, Crap!", channel.name, addresser);
			}
			ping.Stop();
			IrcBot.CloseProgram(writer, reader, m_irc);
		    } else {
			CsBot.CommandHandler.Say("You don't have permissions.", useChannel ? fromChannel : addresser);
		    }
		}
		else if (inputLine.Contains("!") && inputLine.Contains(" :" + settings.command_start + "reload"))
		{
		    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
		    bool useChannel = false;
		    if (inputLine.IndexOf("#") >= 0) {
			useChannel = true;
			fromChannel = inputLine.Substring(inputLine.IndexOf("#")).Split(' ')[0];
		    }

		    if (settings.admins != null && Array.IndexOf(settings.admins, addresser) >= 0) {
			using (var webClient = new System.Net.WebClient()) { 
				Stream setting_file = webClient.OpenRead(SETTINGS_FILE); 
				DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Settings));
				settings = (Settings)js.ReadObject(setting_file);
			}
			//setting_file = new FileStream(SETTINGS_FILE, FileMode.Open);
			//settings = (Settings)js.ReadObject(setting_file);
			//setting_file.Close();
			//setting_file = null;
			CsBot.CommandHandler.settings = settings;
			CsBot.CommandHandler.Say("Reloaded settings from web service.", useChannel ? fromChannel : addresser);
		    } else {
			CsBot.CommandHandler.Say("You don't have permissions.", useChannel ? fromChannel : addresser);
		    }
		}
		else if (inputLine.Contains(settings.command_start) && parsedLine != null && parsedLine.StartsWith(":" + settings.command_start))
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
		    if (isUnderscoreNick)
		    {
			writer.WriteLine("PRIVMSG NickServ :ghost " + settings.nick + " " + settings.password);
			writer.WriteLine("NICK " + settings.nick);
			Console.WriteLine("NICK " + settings.nick);
			ch.HandleMessage(":" + settings.command_start + "say identify " + settings.password, "NickServ", settings.nick);
			isUnderscoreNick = false;
		    }
		    else
		    {
			for (int i = 0; i < settings.channels.Count; i++) 
			{
				if (settings.channels[i].key != "")
				    writer.WriteLine("JOIN " + settings.channels[i].name + " " + settings.channels[i].key);
				else
				{
				    writer.WriteLine("JOIN " + settings.channels[i].name);
				}
				writer.Flush();
			}
		    }
		}
		else if (inputLine.Contains("PONG") && (joined1 || joined2) && !identified)
		{
		    ch.HandleMessage(":" + settings.command_start + "say identify " + settings.password, "NickServ", addresser);
		}
		else if (inputLine.Contains(":Nickname is already in use."))
		{
		    Console.WriteLine("Reopening with _ nick.");
		    writer.Close();
		    reader.Close();
		    m_irc.Close();
		    m_irc = new TcpClient();
		    m_irc.Connect(settings.server, settings.port);
		    if (settings.secure == "1") {
			stream = new SslStream (m_irc.GetStream(), true, new RemoteCertificateValidationCallback (ValidateServerCertificate));
			SslStream sslStream = (SslStream) stream;
			sslStream.AuthenticateAsClient(settings.server);
		    } else {
			stream = m_irc.GetStream();
		    }
		    reader = new StreamReader((Stream)stream);
		    writer = new StreamWriter((Stream)stream);
		    Console.WriteLine(settings.user);
		    // Start PingSender thread
		    ping = new PingSender.cs.PingSender();
		    ping.Start();
		    writer.WriteLine(settings.user);
		    writer.Flush();
		    writer.WriteLine("NICK _" + settings.nick);
		    Console.WriteLine("NICK _" + settings.nick);
		    ch = new CommandHandler(writer, reader);
		    isUnderscoreNick = true;
		}
		else if (inputLine.Contains(settings.nick) && inputLine.Contains("PRIVMSG") && (inputLine.Contains("rock") || inputLine.Contains("paper") || inputLine.Contains("scissors")))
		{
		    Console.WriteLine(inputLine);
		    addresser = inputLine.Substring(inputLine.IndexOf(":") + 1, inputLine.IndexOf("!") - inputLine.IndexOf(":") - 1);
		    string choice = inputLine.Substring(inputLine.LastIndexOf(":") + 1);
		    ch.DirectRoShamBo(choice);

		}
		else if (inputLine.Contains(settings.nick) && inputLine.Contains("PRIVMSG") && inputLine.Contains(":" + settings.command_start))
		{
		    Console.WriteLine("PrivateMessage: " + inputLine);
		    addresser = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
		    string command = inputLine.Substring(inputLine.LastIndexOf(":" + settings.command_start));
		    ch.HandleMessage(command, addresser, addresser);
		}
		else
		{
		    if (inputLine.Contains("PRIVMSG") && inputLine.Contains("!"))
		    {
			string userName = inputLine.Substring(1, inputLine.IndexOf("!") - 1);
			ch.LastMessage(userName, inputLine, fromChannel);
		    }
		}
	    }
	}

	private static Settings ObtainConfig() {
		Console.WriteLine("Trying to pull config.");
		ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
		//System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
		using (var webClient = new System.Net.WebClient()) { 
			Console.WriteLine("Pulling config.");
			Stream setting_file = webClient.OpenRead(SETTINGS_FILE); 
			DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Settings));
			settings = (Settings)js.ReadObject(setting_file);
			Console.WriteLine("Config File:{0}",settings);
		}

                //setting_file.Close();
                //setting_file = null;
		return settings;
	}

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            if(sslPolicyErrors == SslPolicyErrors.None)
                return true;

	    // TODO take this out for actual validation.
	    return true;
            foreach(X509ChainStatus status in chain.ChainStatus) {
                if (certificate.Subject == certificate.Issuer &&
                        status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot &&
                        settings != null && settings.server_validate == false) {
                    continue;
                    
                }
                else if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError &&
                        ((settings != null && settings.server_validate != false) || status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot)) {
                    Console.WriteLine("Certificate not valid: {0}, {1}", sslPolicyErrors, status.StatusInformation);
                    return false;
                }
            }

            return true;
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
                IrcBot.cs.IrcBot.writer.WriteLine(PING + IrcBot.cs.IrcBot.settings.server);
                IrcBot.cs.IrcBot.writer.Flush();
                Thread.Sleep(15000);
            }
        }
    }
}
