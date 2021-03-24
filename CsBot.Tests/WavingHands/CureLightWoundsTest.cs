using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
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

			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);

			Spell cureLightWounds = new CureLightWounds (wizard);
			Assert.True (cureLightWounds.IsMatch (hand, hand2));
		}
	}
}