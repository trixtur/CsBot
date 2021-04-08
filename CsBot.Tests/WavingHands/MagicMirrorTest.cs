using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
using CsBot.Games.WavingHands.Spells.Protections;
using NUnit.Framework;

namespace CsBot.Tests.WavingHands
{
	[TestFixture]
	public class MagicMirrorTest : TestSpell
	{
		[Test]
		public void TestMagicMirror ()
		{
			MakeTestObjects ();

			TestHands ();

			Hand.Add (Gesture.Clap);
			Hand.Add (Gesture.Wave);
			Hand2.Add (Gesture.Clap);
			Hand2.Add (Gesture.Wave);

			Spell magicMirror = new MagicMirror (Wizard);

			Assert.True (magicMirror.IsMatch (Hand, Hand2));
		}
	}
}