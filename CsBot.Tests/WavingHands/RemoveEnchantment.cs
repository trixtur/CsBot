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

			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.ProfferedPalm);

			Spell removeEnchantment = new Games.WavingHands.Spells.Protections.RemoveEnchantment (Wizard);

			Assert.True (removeEnchantment.IsMatch (Hand, Hand2));
		}
	}
}