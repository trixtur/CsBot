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

			hand.Add (Gesture.Wave);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);

			Spell summonGiant = new Giant (wizard);
			Assert.True (summonGiant.IsMatch (hand, hand2));
		}
	}
}