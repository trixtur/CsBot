using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class SurrenderTest : TestSpell
	{
		[Test]
		public void TestSurrender ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.ProfferedPalm);
			Hand2.Add (Gesture.ProfferedPalm);

			Spell surrender = new Surrender ();
			Assert.True (surrender.IsMatch (Hand,Hand2));
		}
	}
}