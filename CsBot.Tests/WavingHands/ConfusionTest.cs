using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ConfusionTest : ShieldTest
	{
		[Test]
		public void TestConfusion ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);

			Spell confusion = new Confusion (Wizard);
			Assert.True (confusion.IsMatch (Hand, Hand2),"Couldn't match Confusion Spell.");
		}
	}
}