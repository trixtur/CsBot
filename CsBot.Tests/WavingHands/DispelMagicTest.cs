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

			Hand.Add (Gesture.Clap);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Wave);

			Hand2.Add (Gesture.Clap);
			Hand2.Add (Gesture.Null);
			Hand2.Add (Gesture.Null);
			Hand2.Add (Gesture.Null);

			Spell dispelMagic = new DispelMagic (Wizard);

			Assert.True (dispelMagic.IsMatch (Hand, Hand2));
		}

	}
}