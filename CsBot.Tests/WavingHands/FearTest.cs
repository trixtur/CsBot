using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class FearTest : TestSpell
	{
		[Test]
		public void TestFear ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.DigitPoint);

			Spell fear = new Fear (Wizard);
			Assert.True (fear.IsMatch (Hand,Hand2));
		}
	}
}