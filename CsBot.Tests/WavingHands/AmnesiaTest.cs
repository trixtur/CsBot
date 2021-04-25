using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class AmnesiaTest : TestSpell
	{
		[Test]
		public void TestAmnesia ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.ProfferedPalm);
			Hand.Add (Gesture.ProfferedPalm);

			Spell amnesia = new Amnesia (Wizard);

			Assert.True (amnesia.IsMatch (Hand, Hand2));
		}
	}
}