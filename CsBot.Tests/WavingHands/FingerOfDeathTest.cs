using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class FingerOfDeathTest : TestSpell
	{
		[Test]
		public void TestFingerOfDeathLeft ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.DigitPoint);

			Spell fingerOfDeath = new FingerOfDeath (Wizard);

			Assert.True (fingerOfDeath.IsMatch (Hand, Hand2));
		}
		[Test]
		public void TestNoMatchFingerOfDeathLeft ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);

			Spell fingerOfDeath = new FingerOfDeath (Wizard);

			Assert.False (fingerOfDeath.IsMatch (Hand, Hand2));
		}
	}
}