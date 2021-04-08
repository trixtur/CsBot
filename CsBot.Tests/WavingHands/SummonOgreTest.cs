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

			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Wave);

			Spell summonOgre = new Ogre (Wizard);
			Assert.True (summonOgre.IsMatch (Hand, Hand2));
		}
	}
}