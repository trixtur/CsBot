using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Damaging;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class IceStormTest : TestSpell
	{
		[Test]
		public void TestIceStorm ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.Clap);

			Hand2.Add (Gesture.Wave);
			Hand2.Add (Gesture.Snap);
			Hand2.Add (Gesture.Snap);
			Hand2.Add (Gesture.Clap);

			Spell iceStorm = new IceStorm (Wizard);
			Assert.True (iceStorm.IsMatch (Hand, Hand2), "Could not match spell.");
		}
	}
}