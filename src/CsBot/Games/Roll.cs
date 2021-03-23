using System;

using CsBot.Interfaces;

namespace CsBot.Games
{
	class Roll : IGame
	{
		readonly CommandHandler _handler;
		int d1, d2, total;
		readonly int DICE = 6;
		readonly Random random;

		public string Name { get; }

		public Roll (CommandHandler handler)
		{
			_handler = handler;
			random = new Random ();

			Name = GetType ().Name;
		}

		public void Play (string command, int endCommand, string verb)
		{
			if (verb != Name.ToLower ()) return;

			var addresser = _handler.GetAddresser ();

			d1 = random.Next (1, DICE + 1);
			d2 = random.Next (1, DICE + 1);

			total = d1 + d2;

			_handler.Say ($"{addresser} rolled a {d1} and a {d2} for a total of {total}"
			);
		}
	}
}
