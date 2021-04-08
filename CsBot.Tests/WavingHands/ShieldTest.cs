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

			Hand.Add (Gesture.ProfferedPalm);

			Spell shield = new Shield (Wizard);

			Assert.True (shield.IsMatch (Hand, Hand2));
		}

		[Test]
		public void TestShieldRight ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand2.Add (Gesture.ProfferedPalm);

			Spell shield = new Shield (Wizard);

			Assert.True (shield.IsMatch (Hand, Hand2));
		}
	}
}