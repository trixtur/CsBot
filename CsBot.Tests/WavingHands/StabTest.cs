using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class StabTest : ShieldTest
	{
		[Test]
		public void TestStab ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Stab);

			Spell stab = new Stab (Wizard);
			Assert.True (stab.IsMatch (Hand, Hand2));
		}

		[Test]
		public void TestStabHand2 ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand2.Add (Gesture.Stab);

			Spell stab = new Stab (Wizard);
			Assert.True (stab.IsMatch (Hand, Hand2));
		}
	}
}