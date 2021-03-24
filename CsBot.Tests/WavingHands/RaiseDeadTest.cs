using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class RaiseDeadTest : TestSpell
	{
		[Test]
		public void TestRaiseDead ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.DigitPoint);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Clap);

			hand2.Add (Gesture.Clap);

			Spell raiseDead = new RaiseDead (wizard);

			Assert.True (raiseDead.IsMatch (hand, hand2));
		}
	}
}