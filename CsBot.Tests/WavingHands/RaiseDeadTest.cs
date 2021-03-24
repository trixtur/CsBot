using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class RaiseDeadTest
	{
		[Test]
		public void TestRaiseDead ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Clap);

			hand2.Add (Gesture.Clap);

			Spell raiseDead = new RaiseDead (wizard);

			Assert.True (raiseDead.IsMatch (hand, hand2));
		}
	}
}