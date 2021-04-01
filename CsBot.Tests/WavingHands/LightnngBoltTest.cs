using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class LightnngBoltTest : TestSpell
	{
		[Test]
		public void TestLightningBoltS1 ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.DigitPoint);

			Spell lightningBolt = new LightningBolt (wizard);

			Assert.True (lightningBolt.IsMatch(hand, hand2));
		}

		[Test]
		public void TestLightningBoltS2 ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.Clap);

			hand2.Add (Gesture.Null);
			hand2.Add (Gesture.Null);
			hand2.Add (Gesture.Null);
			hand2.Add (Gesture.Clap);

			Spell lightningBolt = new LightningBolt (wizard);

			Assert.True (lightningBolt.IsMatch(hand, hand2));

		}
	}
}