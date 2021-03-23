using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class FingerOfDeathTest
	{
		[Test]
		public void TestFingerOfDeathLeft ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

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
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

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