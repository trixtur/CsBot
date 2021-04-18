using CsBot.Games.WavingHands;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class GestureTest
	{
		[Test]
		public void TestP ()
		{
			char current = 'p';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.ProfferedPalm, gesture);
		}

		[Test]
		public void TestF ()
		{
			char current = 'f';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.WigglingFingers, gesture);
		}

		[Test]
		public void TestS ()
		{
			char current = 's';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.Snap, gesture);
		}

		[Test]
		public void TestW ()
		{
			char current = 'w';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.Wave, gesture);
		}

		[Test]
		public void TestD ()
		{
			char current = 'd';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.DigitPoint, gesture);
		}

		[Test]
		public void TestC ()
		{
			char current = 'c';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.Clap, gesture);
		}

		[Test]
		public void TestBadMatch ()
		{
			char current = 'z';
			Gesture gesture = Extensions.GetGesture (current);

			Assert.AreEqual (Gesture.Null, gesture);
		}
	}
}