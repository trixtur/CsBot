using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CureLightWoundsTest : TestSpell
	{
		[Test]
		public void TestCureLightWounds ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);

			Spell cureLightWounds = new CureLightWounds (Wizard);
			Assert.True (cureLightWounds.IsMatch (Hand, Hand2));
		}
	}
}