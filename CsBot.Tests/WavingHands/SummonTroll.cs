using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Summoning;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class SummonTroll : TestSpell
	{
		[Test]
		public void TestSummonTroll ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);

			Spell summonTroll = new Troll (wizard);
			Assert.True (summonTroll.IsMatch (hand, hand2));
		}

	}
}