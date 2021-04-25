using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Summoning;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class SummonGiantTest : TestSpell
	{
		[Test]
		public void TestSummonGiant ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);

			Spell summonGiant = new Giant (Wizard);
			Assert.True (summonGiant.IsMatch (Hand, Hand2));
		}
	}
}