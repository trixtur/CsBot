using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Enchantments;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class ProtectionFromEvilTest : TestSpell
	{
		[Test]
		public void TestProtectionFromEvil ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.Wave);
			Hand.Add (Gesture.ProfferedPalm);

			Spell protectionFromEvil = new ProtectionFromEvil (Wizard);
			Assert.True (protectionFromEvil.IsMatch (Hand, Hand2));
		}
	}
}