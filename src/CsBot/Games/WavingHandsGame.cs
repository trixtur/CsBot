
using CsBot.Interfaces;

namespace CsBot.Games
{
	class WavingHandsGame : IGame
	{
		readonly CommandHandler _handler;

		public string Name { get; }

		public WavingHandsGame (CommandHandler handler)
		{
			_handler = handler;
			Name = GetType ().Name;
		}

		public void Play (string command, int endCommand, string verb)
		{
			throw new System.NotImplementedException ();
		}
	}
}
