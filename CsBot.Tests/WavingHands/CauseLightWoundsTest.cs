using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class CauseLightWoundsTest : TestSpell
	{
		[Test]
		public void TestCauseLightWounds ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.ProfferedPalm);

			Spell causeLightWounds = new CauseLightWounds (wizard);
			Assert.True (causeLightWounds.IsMatch (hand, hand2));
		}
	}
}