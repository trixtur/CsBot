using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ParalysisTest : TestSpell
	{
		[Test]
		public void TestParalysis ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.WigglingFingers);

			Spell paralysis = new Paralysis (Wizard);
			Assert.True (paralysis.IsMatch (Hand, Hand2),"Unable to match Paralysis Sequence.");
		}
	}
}