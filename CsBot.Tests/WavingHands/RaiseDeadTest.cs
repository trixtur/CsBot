using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
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

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Clap);

			Hand2.Add (Gesture.Clap);

			Spell raiseDead = new RaiseDead (Wizard);

			Assert.True (raiseDead.IsMatch (Hand, Hand2));
		}
	}
}