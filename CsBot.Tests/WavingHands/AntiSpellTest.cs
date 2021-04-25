using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class AntiSpellTest : TestSpell
	{
		[Test]
		public void TestAntiSpell ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.WigglingFingers);

			Spell antiSpell = new AntiSpell (Wizard);
			Assert.True (antiSpell.IsMatch (Hand,Hand2));
		}
	}
}