using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class MissileTest : TestSpell
	{
		[Test]
		public void TestMissileLeft ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.DigitPoint);

			Spell missile = new Missile (Wizard);

			Assert.True (missile.IsMatch (Hand, Hand2));
		}

		[Test]
		public void TestMissileRight ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand2.Add (Gesture.Snap);
			Hand2.Add (Gesture.DigitPoint);

			Spell missile = new Missile (Wizard);

			Assert.True (missile.IsMatch (Hand, Hand2));
		}
	}
}