using CsBot.Games.WavingHands;

using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class WavingHands
	{
		[Test]
		public void TestHandAdd ()
		{
			var hand = new Hand (Hand.Side.Left);

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.DigitPoint);

			Gesture[] last4 = hand.GetLast (4);
			Assert.AreEqual (Gesture.Clap, last4[0]);
			Assert.AreEqual (Gesture.Snap, last4[1]);
			Assert.AreEqual (Gesture.Wave, last4[2]);
			Assert.AreEqual (Gesture.DigitPoint, last4[3]);
		}

		[Test]
		public void TestGetAt ()
		{
			Hand hand = new Hand (Hand.Side.Left);

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.DigitPoint);

			Gesture result = hand.GetAt (7);

			Assert.AreEqual (result, Gesture.Snap);
		}
	}
}