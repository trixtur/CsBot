using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class PoisonTest : TestSpell
	{
		[Test]
		public void TestPoison ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.DigitPoint);

			Spell poison = new Poison (Wizard);
			Assert.True (poison.IsMatch (Hand,Hand2));
		}
	}
}