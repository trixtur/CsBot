using NUnit.Framework;
using CsBot.Games.WHands;

namespace CsBot.Tests
{
	[TestFixture]
	public class WavingHands
	{
		[Test]
		public void TestHandAdd ()
		{
			Hand h = new Hand (Hand.Side.left);

			Hand.Side s = h.GetSide ();
			Assert.AreEqual (s, Hand.Side.left);

			h.Add ('t');
			h.Add ('e');
			h.Add ('s');
			h.Add ('t');

			char[] last4 = h.GetLast (4);
			Assert.AreEqual ('t', last4[0]);
			Assert.AreEqual ('e', last4[1]);
			Assert.AreEqual ('s', last4[2]);
			Assert.AreEqual ('t', last4[3]);
		}
	}
}