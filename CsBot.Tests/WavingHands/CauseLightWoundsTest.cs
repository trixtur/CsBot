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

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.ProfferedPalm);

			Spell causeLightWounds = new CauseLightWounds (Wizard);
			Assert.True (causeLightWounds.IsMatch (Hand, Hand2));
		}
	}
}