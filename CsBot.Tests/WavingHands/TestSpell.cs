using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	public abstract class TestSpell
	{
		protected Hand hand, hand2;
		protected Living wizard;

		protected void MakeTestObjects ()
		{
			hand = new Hand (Hand.Side.Left);
			hand2 = new Hand (Hand.Side.Right);

			wizard = new Wizard ();
		}

		protected void TestHands ()
		{
			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);
		}
	}
}