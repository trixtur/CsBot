using CsBot.Games.WavingHands;

using NUnit.Framework;

namespace CsBot.Tests
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

			hand.Add ('t');
			hand.Add ('e');
			hand.Add ('s');
			hand.Add ('t');

			char[] last4 = hand.GetLast (4);
			Assert.AreEqual ('t', last4[0]);
			Assert.AreEqual ('e', last4[1]);
			Assert.AreEqual ('s', last4[2]);
			Assert.AreEqual ('t', last4[3]);
		}

		[Test]
		public void TestGetAt ()
		{
			Hand h = new Hand (Hand.Side.Left);

			Hand.Side s = h.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);

			h.Add ('t');
			h.Add ('e');
			h.Add ('s');
			h.Add ('t');

			char result = h.GetAt (7);

			Assert.AreEqual (result, 'e');
		}
	}
}