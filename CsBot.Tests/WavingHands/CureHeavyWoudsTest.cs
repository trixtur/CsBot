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

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Wave);

			Spell cureHeavyWounds = new CureHeavyWounds (Wizard);
			Assert.True (cureHeavyWounds.IsMatch (Hand, Hand2));
		}
	}
}