using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ResistHeatTest : TestSpell
	{
		[Test]
		public void TestResistHeat ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.ProfferedPalm);

			Spell resistHeat = new ResistHeat (Wizard);
			Assert.True (resistHeat.IsMatch (Hand, Hand2));
		}
	}
}