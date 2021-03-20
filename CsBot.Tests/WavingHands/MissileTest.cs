using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class MissileTest
	{
		[Test]
		public void TestMissileLeft ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.DigitPoint);

			Spell missile = new Missile (wizard);

			Assert.True (missile.IsMatch (hand, hand2));
		}

		[Test]
		public void TestMissileRight ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand2.Add (Gesture.Snap);
			hand2.Add (Gesture.DigitPoint);

			Spell missile = new Missile (wizard);

			Assert.True (missile.IsMatch (hand, hand2));
		}
	}
}