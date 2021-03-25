using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ShieldTest : TestSpell
	{
		[Test]
		public void TestShieldLeft ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.ProfferedPalm);

			Spell shield = new Shield (wizard);

			Assert.True (shield.IsMatch (hand, hand2));
		}

		[Test]
		public void TestShieldRight ()
		{
			MakeTestObjects ();

			TestHands ();

			hand2.Add (Gesture.ProfferedPalm);

			Spell shield = new Shield (wizard);

			Assert.True (shield.IsMatch (hand, hand2));
		}
	}
}