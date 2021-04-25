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

			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);

			Spell summonTroll = new Troll (Wizard);
			Assert.True (summonTroll.IsMatch (Hand, Hand2));
		}

	}
}