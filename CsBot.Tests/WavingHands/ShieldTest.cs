using CsBot.Games.WavingHands;

using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ShieldTest
	{
		[Test]
		public void TestShieldLeft ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.DigitPoint);
		}
	}
}