using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class DiseaseTest : TestSpell
	{
		[Test]
		public void TestDisease ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.DigitPoint);
			Hand.Add (Gesture.Snap);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.WigglingFingers);
			Hand.Add (Gesture.Clap);

			Hand2.Add (Gesture.DigitPoint);
			Hand2.Add (Gesture.Snap);
			Hand2.Add (Gesture.WigglingFingers);
			Hand2.Add (Gesture.WigglingFingers);
			Hand2.Add (Gesture.WigglingFingers);
			Hand2.Add (Gesture.Clap);

			Spell disease = new Disease (Wizard);
			Assert.True (disease.IsMatch (Hand,Hand2));
		}
	}
}