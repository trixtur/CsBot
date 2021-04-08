using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CharmPersonTest : TestSpell
	{
		[Test]
		public void TestCharmPerson ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.WigglingFingers);

			Spell charmPerson = new CharmPerson (Wizard);
			Assert.True (charmPerson.IsMatch (Hand, Hand2), "Unable to match Charm Person Enchantment Sequence.");
		}
	}
}