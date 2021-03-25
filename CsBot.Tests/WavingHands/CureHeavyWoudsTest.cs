using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CureHeavyWoudsTest : TestSpell
	{
		[Test]
		public void TestCureHeavyWounds ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Wave);

			Spell cureHeavyWounds = new CureHeavyWounds (wizard);
			Assert.True (cureHeavyWounds.IsMatch (hand, hand2));
		}
	}
}