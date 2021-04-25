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

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.DigitPoint);

			Spell causeHeavyWounds = new CauseHeavyWounds (Wizard);
			Assert.True (causeHeavyWounds.IsMatch (Hand, Hand2));
		}
	}
}