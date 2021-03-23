using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class MagicMirrorTest
	{
		[Test]
		public void TestMagicMirror ()
		{
			var hand = new Hand (Hand.Side.Left);
			var hand2 = new Hand (Hand.Side.Right);
			var wizard = new Wizard ();

			Hand.Side s = hand.GetSide ();
			Assert.AreEqual (s, Hand.Side.Left);
			Hand.Side s2 = hand2.GetSide ();
			Assert.AreEqual (s2, Hand.Side.Right);

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.Wave);
			hand2.Add (Gesture.Clap);
			hand2.Add (Gesture.Wave);

			Spell magicMirror = new MagicMirror (wizard);

			Assert.True (magicMirror.IsMatch (hand, hand2));
		}
	}
}