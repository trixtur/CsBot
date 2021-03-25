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

			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.DigitPoint);

			Spell fingerOfDeath = new FingerOfDeath (wizard);

			Assert.True (fingerOfDeath.IsMatch (hand, hand2));
		}
		[Test]
		public void TestNoMatchFingerOfDeathLeft ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Snap);

			Spell fingerOfDeath = new FingerOfDeath (wizard);

			Assert.False (fingerOfDeath.IsMatch (hand, hand2));
		}
	}
}