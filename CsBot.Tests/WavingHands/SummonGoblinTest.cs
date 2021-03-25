using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Summoning;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class SummonGoblinTest : TestSpell
	{
		[Test]
		public void TestSummonGoblin ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.Snap);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);

			Spell summonGoblin = new Goblin (wizard);
			Assert.True (summonGoblin.IsMatch (hand, hand2));
		}
	}
}