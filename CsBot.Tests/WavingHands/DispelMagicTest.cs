using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class DispelMagicTest : TestSpell
	{
		[Test]
		public void TestDispelMagic ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Wave);

			hand2.Add (Gesture.Clap);
			hand2.Add (Gesture.Null);
			hand2.Add (Gesture.Null);
			hand2.Add (Gesture.Null);

			Spell dispelMagic = new DispelMagic (wizard);

			Assert.True (dispelMagic.IsMatch (hand, hand2));
		}

	}
}