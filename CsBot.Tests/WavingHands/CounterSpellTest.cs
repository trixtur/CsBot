using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CounterSpellTest : TestSpell
	{
		[Test]
		public void TestCounterSpellS1 ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.ProfferedPalm);

			Spell counterSpell = new CounterSpell (wizard);

			Assert.True (counterSpell.IsMatch (hand, hand2));
		}
		[Test]
		public void TestCounterSpellS2 ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Snap);

			Spell counterSpell = new CounterSpell (wizard);

			Assert.True (counterSpell.IsMatch (hand, hand2));
		}
	}
}