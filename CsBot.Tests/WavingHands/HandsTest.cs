using CsBot.Games.WavingHands;

using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class WavingHands : TestSpell
	{
		[Test]
		public void TestHandAdd ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Clap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.DigitPoint);

			Gesture[] last4 = Hand.GetLast (4);
			Assert.AreEqual (Gesture.Clap, last4[0]);
			Assert.AreEqual (Gesture.Snap, last4[1]);
			Assert.AreEqual (Gesture.Wave, last4[2]);
			Assert.AreEqual (Gesture.DigitPoint, last4[3]);
		}

		[Test]
		public void TestGetAt ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Clap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.DigitPoint);

			Gesture result = Hand.GetAt (7);

			Assert.AreEqual (result, Gesture.Snap);
		}
	}
}