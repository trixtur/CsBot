using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Summoning;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class SummonOgreTest : TestSpell
	{
		[Test]
		public void TestSummonOgre ()
		{
			MakeTestObjects ();

			TestHands ();

			hand.Add (Gesture.ProfferedPalm);
			hand.Add (Gesture.Snap);
			hand.Add (Gesture.WigglingFingers);
			hand.Add (Gesture.Wave);

			Spell summonOgre = new Ogre (wizard);
			Assert.True (summonOgre.IsMatch (hand, hand2));
		}
	}
}