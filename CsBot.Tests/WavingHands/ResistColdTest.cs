using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ResistColdTest : TestSpell
	{
		[Test]
		public void TestResistCold ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.ProfferedPalm);

			Spell resistCold = new ResistCold (Wizard);
			Assert.True (resistCold.IsMatch (Hand,Hand2));
		}
	}
}