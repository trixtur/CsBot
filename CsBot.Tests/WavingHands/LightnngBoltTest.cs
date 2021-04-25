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

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.DigitPoint);

			Spell lightningBolt = new LightningBolt (Wizard);

			Assert.True (lightningBolt.IsMatch(Hand, Hand2));
		}

		[Test]
		public void TestLightningBoltS2 ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.Clap);

			Hand2.Add (Gesture.Null);
			Hand2.Add (Gesture.Null);
			Hand2.Add (Gesture.Null);
			Hand2.Add (Gesture.Clap);

			Spell lightningBolt = new LightningBolt (Wizard);

			Assert.True (lightningBolt.IsMatch(Hand, Hand2));

		}
	}
}