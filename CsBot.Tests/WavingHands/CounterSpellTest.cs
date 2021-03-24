using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CounterSpellTest
	{
		[Test]
		public void TestCounterSpellS1 ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.ProfferedPalm);

			Spell counterSpell = new CounterSpell (wizard);

			Assert.True (counterSpell.IsMatch (hand, hand2));
		}
		[Test]
		public void TestCounterSpellS2 ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Snap);

			Spell counterSpell = new CounterSpell (wizard);

			Assert.True (counterSpell.IsMatch (hand, hand2));
		}
	}
}