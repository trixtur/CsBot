using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
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

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.ProfferedPalm);

			Spell counterSpell = new CounterSpell (Wizard);

			Assert.True (counterSpell.IsMatch (Hand, Hand2));
		}
		[Test]
		public void TestCounterSpellS2 ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Snap);

			Spell counterSpell = new CounterSpell (Wizard);

			Assert.True (counterSpell.IsMatch (Hand, Hand2));
		}
	}
}