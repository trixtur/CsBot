using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class DispelMagicTest
	{
		[Test]
		public void TestDispelMagic ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Wave);

			Spell dispelMagic = new DispelMagic (wizard);

			Assert.True (dispelMagic.IsMatch (hand, hand2));
		}

	}
}