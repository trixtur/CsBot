using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class FireballTest : ShieldTest
	{
		[Test]
		public void TestFireBall ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.DigitPoint);

			Spell fireball = new Fireball (wizard);
			Assert.True (fireball.IsMatch (hand, hand2));
		}
	}
}