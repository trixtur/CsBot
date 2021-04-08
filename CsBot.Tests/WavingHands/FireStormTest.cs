using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class FireStormTest : TestSpell
	{
		[Test]
		public void TestFireStorm ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Clap);

			Hand2.Add (Gesture.Snap); // Garbage gesture
			Hand2.Add (Gesture.Snap); // Garbage gesture
			Hand2.Add (Gesture.Snap); // Garbage gesture
			Hand2.Add (Gesture.Clap);

			Spell firestorm = new FireStorm (Wizard);
			Assert.True (firestorm.IsMatch (Hand, Hand2));
		}

		[Test]
		public void TestFireStormFailEmptySecondSequence ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Clap);

			Spell firestorm = new FireStorm (Wizard);
			Assert.False (firestorm.IsMatch (Hand, Hand2));
		}

		[Test]
		public void TestFireStormBadSecondSequence ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Clap);

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Snap);

			Spell firestorm = new FireStorm (Wizard);
			Assert.False (firestorm.IsMatch (Hand, Hand2));
		}
	}
}