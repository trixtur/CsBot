using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	public abstract class TestSpell
	{
		protected Hand Hand, Hand2;
		protected Living Wizard;

		protected void MakeTestObjects ()
		{
			Hand = new Hand (Hand.Side.Left);
			Hand2 = new Hand (Hand.Side.Right);

			Wizard = new Wizard ();
		}

		protected void TestHands ()
		{
			Hand.Side s = Hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = Hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);
		}
	}
}