using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class IceStormTest : TestSpell
	{
		[Test]
		public void TestIceStorm ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Clap);

			hand2.Add (Gesture.Wave);
			hand2.Add (Gesture.Snap);
			hand2.Add (Gesture.Snap);
			hand2.Add (Gesture.Clap);

			Spell iceStorm = new IceStorm (wizard);
			Assert.True (iceStorm.IsMatch (hand, hand2), "Could not match spell.");
		}
	}
}