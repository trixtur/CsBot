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

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.DigitPoint);

			Spell missile = new Missile (wizard);

			Assert.True (missile.IsMatch (hand, hand2));
		}

		[Test]
		public void TestMissileRight ()
		{
			MakeTestObjects ();

			TestHands ();

			hand2.Add (Gesture.Snap);
			hand2.Add (Gesture.DigitPoint);

			Spell missile = new Missile (wizard);

			Assert.True (missile.IsMatch (hand, hand2));
		}
	}
}