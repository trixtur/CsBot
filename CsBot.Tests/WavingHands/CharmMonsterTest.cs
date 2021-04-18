using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CharmMonsterTest : TestSpell
	{
		[Test]
		public void TestCharmMonster ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.DigitPoint);

			Spell charmMonster = new CharmMonster (Wizard);
			Assert.True (charmMonster.IsMatch (Hand, Hand2));
		}

	}
}