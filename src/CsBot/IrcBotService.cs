using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using CsBot.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace CsBot
{
	/// <summary>
	/// This program establishes a connection to an irc server, joins
	/// a channel and greets every nickname that joins the channel.
	/// </summary>
	class IrcBotService : IIrcBotService
	{
		readonly ILogger<IrcBotService> _log;
		readonly IConfiguration _config;
		public IrcServerOptions Settings { get; set; }

		readonly string _settingsFileLocation;

		// StreamWriter is declared here so that PingSender can access it
		public StreamWriter Writer { get; private set; }
		public StreamReader Reader { get; private set; }

		CommandHandler commandHandler;
		TcpClient ircTcpClient;
		static bool isUnderscoreNick;

		public IrcBotService (ILogger<IrcBotService> log, IConfiguration config)
		{
			_log = log;
			_config = config;
			_settingsFileLocation = _config.GetValue<string> (Constants.IrcConfigLocationKey) ?? "127.0.0.1";

			commandHandler = new CommandHandler (this);
		}

		public async Task Run ()
		{
			var nickname = "";
			var addresser = "";

			try {
				await ObtainIrcConfig ();
				if (string.IsNullOrEmpty (Settings.Password)) {
					Console.Write ("Password: ");
					Settings.Password = SecureStringToString(ReadPasswordLine ());
				}

				var fromChannel = Settings.Channels[0].Name;
				using var irc = ircTcpClient = new TcpClient ();

				_log.LogInformation ($"Trying to connect to server {Settings.Server}.");
				await ircTcpClient.ConnectAsync (Settings.Server, Settings.Port);

				Stream stream;
				if (Settings.Secure) {
					stream = new SslStream (ircTcpClient.GetStream (), true, ValidateServerCertificate);
					await ((SslStream)stream).AuthenticateAsClientAsync (Settings.Server);
				} else {
					stream = ircTcpClient.GetStream ();
				}

				Reader = new StreamReader (stream);
				Writer = new StreamWriter (stream);
				_log.LogInformation (Settings.User);
				// Run PingSender thread
				var ping = new PingSender (this);
				ping.Start ();
				Writer.WriteLine (Settings.User);
				Writer.Flush ();
				Writer.WriteLine ($"NICK {Settings.Nick}");
				Writer.Flush ();
				Writer.WriteLine ($"PASS {Settings.Password}");
				Writer.Flush ();
				Console.WriteLine ($"NICK {Settings.Nick}");
				commandHandler = new CommandHandler (this);
                Console.WriteLine($"Username: {Settings.Nick}");
                Console.WriteLine($"Password: {Settings.Password}");
				Writer.WriteLine ($"PRIVMSG mattermost LOGIN {Settings.Nick} {Settings.Password}");
				Writer.Flush ();
				//Writer.WriteLine("JOIN " + ircServerOptions.channels[0].Name + " " + KEY);
				//Writer.WriteLine("JOIN " + ircServerOptions.channels[0].Name);
				//Writer.Flush();
				//Writer.WriteLine("JOIN " + ircServerOptions.channels[0].name2);
				//Writer.Flush();

				await EventLoop (addresser, fromChannel, ping, nickname, stream);
			} catch (Exception e) {
				// Show the exception, sleep for a while and try to establish a new connection to irc server
				_log.LogError ($"Exception info: {e}");
				await Task.Delay (5000);

				commandHandler.HandleMessage ($":{Settings.CommandStart}say Awe, Crap!", "#bots", "self");

				_ = Run ();
			} finally {
				// Close all streams
				Writer?.Close ();
				Reader?.Close ();
				ircTcpClient?.Close ();
			}
		}

		void CloseProgram ()
		{
			// Close all streams
			Writer.Close ();
			Reader.Close ();
			ircTcpClient.Close ();
			Environment.Exit (0);
		}

		async Task EventLoop (string addresser, string fromChannel, PingSender ping, string nickname, object stream)
		{
			var joined = false;
			var identified = true;

			while (true) {
				var inputLine = Reader.ReadLine ();
				string parsedLine = null;

				foreach (var channel in Settings.Channels) {
					if (inputLine.Contains (channel.Name) && inputLine.IndexOf ("#") > -1)
						fromChannel = inputLine.Substring (inputLine.IndexOf ("#")).Split (' ')[0];

					if (inputLine.Contains ($"{Settings.Nick} = {channel.Name}") || inputLine.Contains (
							$"{Settings.Nick} = {channel.Name}"))
						//if (inputLine.Contains(ircServerOptions.nick + " = " + ircServerOptions.channels[0].Name))
						commandHandler.ParseUsers (inputLine);

					//if (inputLine.Contains(ircServerOptions.channels[0].Name))
					if (joined && !inputLine.EndsWith (fromChannel)) {
						//parsedLine = inputLine.Substring(inputLine.IndexOf(FromChannel) + FromChannel.Length + 1);
						if (!inputLine.EndsWith (channel.Name) && (parsedLine == null || !parsedLine.StartsWith (
							$":{Settings.CommandStart}")) && channel.Name == fromChannel)
							parsedLine = inputLine.Substring (inputLine.IndexOf (fromChannel) + channel.Name.Length + 1).Trim ();
					}
				}

				if (!joined)
					Console.WriteLine (inputLine);

				if (inputLine.Contains (Constants.NICK)) {
					var origUser = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
					var newUser = inputLine.Substring (inputLine.IndexOf (Constants.NICK) + 6);
					commandHandler.UpdateUserName (origUser, newUser);
				}

				if (inputLine.EndsWith ($"JOIN {fromChannel}")) {
					// Parse nickname of person who joined the channel
					nickname = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
					if (nickname == Settings.Nick) {
						if (fromChannel == Settings.Channels[0].Name)
							joined = true;

						if (fromChannel == "#bots") {
							commandHandler.HandleMessage ($":{Settings.CommandStart}say I'm back baby!", fromChannel, addresser);
						}
						continue;
					}
					// Welcome the nickname to channel by sending a notice
					Writer.WriteLine ($"NOTICE {nickname}: Hi {nickname} and welcome to {fromChannel} channel!");
					commandHandler.HandleMessage (
						$":{Settings.CommandStart}say {nickname}: Hi and welcome to {fromChannel} channel!", fromChannel, addresser);
					commandHandler.AddUser (nickname);
					Writer.Flush ();
					// Sleep to prevent excess flood
					await Task.Delay (2000);
				} else if (inputLine.StartsWith (":NickServ") && inputLine.Contains ("You are now identified")) {
					identified = true;
					Console.WriteLine (inputLine);
				} else if (inputLine.Contains ("!") && inputLine.Contains ($" :{Settings.CommandStart}quit")) {
					addresser = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
					var useChannel = false;
					if (inputLine.Contains ("#")) {
						useChannel = true;
						fromChannel = inputLine.Substring (inputLine.IndexOf ("#")).Split (' ')[0];
					}

					if (Settings.Admins != null && Settings.Admins.Contains (addresser)) {
						commandHandler.HandleMessage ($":{Settings.CommandStart}say Awe, Crap!", "#bots", addresser);

						ping.Stop ();
						CloseProgram ();
					} else {
						commandHandler.Say ("You don't have permissions.", useChannel ? fromChannel : addresser);
					}
				} else if (inputLine.Contains ("!") && inputLine.Contains ($" :{Settings.CommandStart}reload")) {
					addresser = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
					var useChannel = false;
					if (inputLine.Contains ("#")) {
						useChannel = true;
						fromChannel = inputLine.Substring (inputLine.IndexOf ("#")).Split (' ')[0];
					}

					if (Settings.Admins != null && Settings.Admins.Contains (addresser)) {
						await ObtainIrcConfig ();
						commandHandler.Say ("Reloaded ircServerOptions from web service.", useChannel ? fromChannel : addresser);
					} else {
						commandHandler.Say ("You don't have permissions.", useChannel ? fromChannel : addresser);
					}
				} else if (inputLine != null && inputLine.Contains (Settings.CommandStart) && parsedLine != null &&
				           parsedLine.StartsWith ( $":{Settings.CommandStart}")) {
					addresser = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
					fromChannel = inputLine.Substring (inputLine.IndexOf ("#")).Split (' ')[0];
					commandHandler.HandleMessage (parsedLine, fromChannel, addresser);
				} else if (inputLine.StartsWith (Constants.PING)) {
					Writer.WriteLine (Constants.PONG + inputLine.Substring (inputLine.IndexOf (":") + 1));
					Writer.Flush ();
				} else if (inputLine.Contains ("PONG") && (!joined)) {
					if (isUnderscoreNick) {
						Writer.WriteLine ($"PRIVMSG NickServ :ghost {Settings.Nick} {Settings.Password}");
						Writer.WriteLine ($"NICK {Settings.Nick}");
						Console.WriteLine ($"NICK {Settings.Nick}");
						commandHandler.HandleMessage ($":{Settings.CommandStart}say identify {Settings.Password}", "NickServ", Settings.Nick);
						isUnderscoreNick = false;
					} else {
						foreach (var channel in Settings.Channels) {
							if (channel.Key != "")
								Writer.WriteLine ($"JOIN {channel.Name} {channel.Key}");
							else
								Writer.WriteLine ($"JOIN {channel.Name}");

							Writer.Flush ();
						}
					}
				} else if (inputLine.Contains ("PONG") && (joined) && !identified) {
					commandHandler.HandleMessage ($":{Settings.CommandStart}say identify {Settings.Password}", "NickServ", addresser);
				} else if (inputLine.Contains ("LOGIN")) {
					Console.WriteLine ($"{Settings.Nick} and {Settings.Password}");
					Writer.WriteLine ($"PRIVMSG mattermost LOGIN {Settings.Nick} {Settings.Password}");
					Writer.Flush ();
				} else if (inputLine.Contains (":Nickname is already in use.")) {
					Console.WriteLine ("Reopening with _ nick.");

					Writer.Close ();
					Reader.Close ();
					ircTcpClient.Close ();
					ircTcpClient = new TcpClient ();
					ircTcpClient.Connect (Settings.Server, Settings.Port);
					if (Settings.Secure) {
						stream = new SslStream (ircTcpClient.GetStream (), true, ValidateServerCertificate);
						var sslStream = (SslStream)stream;
						sslStream.AuthenticateAsClient (Settings.Server);
					} else {
						stream = ircTcpClient.GetStream ();
					}

					Reader = new StreamReader ((Stream)stream);
					Writer = new StreamWriter ((Stream)stream);
					Console.WriteLine (Settings.User);
					// Run PingSender thread
					ping = new PingSender (this);
					ping.Start ();
					Writer.WriteLine (Settings.User);
					Writer.Flush ();
					Writer.WriteLine ($"NICK _{Settings.Nick}");
					Console.WriteLine ($"NICK _{Settings.Nick}");
					commandHandler = new CommandHandler (this);
					isUnderscoreNick = true;
				} else if (inputLine.Contains ("PRIVMSG") && (inputLine.Contains ("rock") || inputLine.Contains ("paper") || inputLine.Contains ("scissors"))) {
					Console.WriteLine (inputLine);
					addresser = inputLine.Substring (inputLine.IndexOf (":") + 1, inputLine.IndexOf ("!") - inputLine.IndexOf (":") - 1);
					var choice = inputLine.Substring (inputLine.LastIndexOf (":") + 1);
					commandHandler.DirectRoShamBo (choice);

				} else if (inputLine.Contains ("PRIVMSG") && inputLine.Contains ($":{Settings.CommandStart}")) {
					Console.WriteLine ($"PrivateMessage: {inputLine}");
					addresser = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
					var command = inputLine.Substring (inputLine.LastIndexOf ($":{Settings.CommandStart}"));
					commandHandler.HandleMessage (command, addresser, addresser);
				} else {
					if (inputLine.Contains ("PRIVMSG") && inputLine.Contains ("!")) {
						var userName = inputLine.Substring (1, inputLine.IndexOf ("!") - 1);
						commandHandler.LastMessage (userName, inputLine, fromChannel);
					}
				}
			}
		}

		async Task ObtainIrcConfig ()
		{
			// FIXME, the config stuff needs to be reviewed
			_log.LogInformation ("Trying to pull config.");
			_log.LogInformation ($"Settings file: {Constants.SettingsFile}");

			string settingsJson;
			if (_settingsFileLocation.StartsWith ("http"))
				settingsJson = await DownloadIrcConfig (new Uri (_settingsFileLocation));
			else
				settingsJson = LoadLocalIrcConfig (Path.Combine (Directory.GetCurrentDirectory (), _settingsFileLocation));

			Settings = JsonConvert.DeserializeObject<IrcServerOptions> (settingsJson);

			_log.LogInformation ("Settings Loaded");
		}

		async Task<string> DownloadIrcConfig (Uri uri)
		{
			_log.LogInformation ($"Downloading from {uri}");

			using var httpClient = new HttpClient { BaseAddress = uri };
			var json = await httpClient.GetStringAsync (_settingsFileLocation);

			return json;
		}

		string LoadLocalIrcConfig (string location)
		{
			_log.LogInformation ($"Loading from {location}");

			if (!File.Exists (location)) {
				_log.LogError ($"File {location} doesn't exist");
				return "{}";
			}

			var json = File.ReadAllText (location);

			return json;
		}

		bool ValidateServerCertificate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
				return true;

			// TODO uncomment for actual validation.
			//foreach (X509ChainStatus status in chain.ChainStatus)
			//{
			//    if (certificate.Subject == certificate.Issuer &&
			//            status.Status == X509ChainStatusFlags.UntrustedRoot &&
			//            Settings != null && Settings.ServerValidate == false)
			//    {
			//        continue;

			//    }
			//    else if (status.Status != X509ChainStatusFlags.NoError &&
			//            ((Settings != null && Settings.ServerValidate != false) || status.Status != X509ChainStatusFlags.UntrustedRoot))
			//    {
			//        Console.WriteLine("Certificate not valid: {0}, {1}", sslPolicyErrors, status.StatusInformation);
			//        return false;
			//    }
			//}

			return true;
		}

		static SecureString ReadPasswordLine()
		{
			var password = new SecureString ();

			ConsoleKeyInfo key;
			do
			{
				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					continue;

				if (!(key.KeyChar < ' '))
				{
					password.AppendChar (key.KeyChar);
					Console.Write("*");
				}
				else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
				{
					Console.Write(Convert.ToChar(ConsoleKey.Backspace));
					password.RemoveAt (password.Length - 1);
					Console.Write(" ");
					Console.Write(Convert.ToChar(ConsoleKey.Backspace));
				}
			} while (key.Key != ConsoleKey.Enter);

			return password;
		}

        String SecureStringToString(SecureString value) {
            IntPtr valuePtr = IntPtr.Zero;
            try {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
	}
}
