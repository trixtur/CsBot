using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class RemoveEnchantment : TestSpell
	{
		[Test]
		public void TestRemoveEnchantment ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.ProfferedPalm);

			Spell removeEnchantment = new Games.WavingHands.Spells.RemoveEnchantment (wizard);

			Assert.True (removeEnchantment.IsMatch (hand, hand2));
		}
	}
}