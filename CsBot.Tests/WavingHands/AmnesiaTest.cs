using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Enchantments;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class AmnesiaTest : TestSpell
	{
		[Test]
		public void TestAmnesia ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.ProfferedPalm);

			Spell amnesia = new Amnesia (wizard);

			Assert.True (amnesia.IsMatch (hand, hand2));
		}
	}
}