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

			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);

			Spell summonGoblin = new Goblin (Wizard);
			Assert.True (summonGoblin.IsMatch (Hand, Hand2));
		}
	}
}