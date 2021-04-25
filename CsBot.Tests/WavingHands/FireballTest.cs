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

			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.DigitPoint);

			Spell fireball = new Fireball (Wizard);
			Assert.True (fireball.IsMatch (Hand, Hand2));
		}
	}
}