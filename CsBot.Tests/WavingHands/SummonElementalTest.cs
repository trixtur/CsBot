using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Summoning;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class SummonElementalTest : TestSpell
	{
		[Test]
		public void TestSummonElemental ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Snap);

			hand2.Add (Gesture.Clap);

			// The rest of hand 2 can be any Gestures,
			// We don't care.
			hand2.Add (Gesture.Snap);
			hand2.Add (Gesture.Wave);
			hand2.Add (Gesture.Wave);
			hand2.Add (Gesture.Snap);

			Spell summonElemental = new Elemental (wizard);
			Assert.True (summonElemental.IsMatch(hand, hand2));
		}
	}
}