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

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Clap);

			hand2.Add (Gesture.Snap); // Garbage gesture
			hand2.Add (Gesture.Snap); // Garbage gesture
			hand2.Add (Gesture.Snap); // Garbage gesture
			hand2.Add (Gesture.Clap);

			Spell firestorm = new FireStorm (wizard);
			Assert.True (firestorm.IsMatch (hand, hand2));
		}

		[Test]
		public void TestFireStormFailEmptySecondSequence ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Clap);

			Spell firestorm = new FireStorm (wizard);
			Assert.False (firestorm.IsMatch (hand, hand2));
		}

		[Test]
		public void TestFireStormBadSecondSequence ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Clap);

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Wave);
			hand.Add (Gesture.Snap);

			Spell firestorm = new FireStorm (wizard);
			Assert.False (firestorm.IsMatch (hand, hand2));
		}
	}
}