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

			Hand.Add (Gesture.Clap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Snap);

			Hand2.Add (Gesture.Clap);

			// The rest of hand 2 can be any Gestures,
			// We don't care.
			Hand2.Add (Gesture.Snap);
			Hand2.Add (Gesture.Wave);
			Hand2.Add (Gesture.Wave);
			Hand2.Add (Gesture.Snap);

			Spell summonElemental = new Elemental (Wizard);
			Assert.True (summonElemental.IsMatch(Hand, Hand2));
		}
	}
}