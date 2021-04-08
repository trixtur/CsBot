using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CauseHeavyWoundsTest : ShieldTest
	{
		[Test]
		public void TestCauseHeavyWounds ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.DigitPoint);

			Spell causeHeavyWounds = new CauseHeavyWounds (wizard);
			Assert.True (causeHeavyWounds.IsMatch (hand, hand2));
		}
	}
}