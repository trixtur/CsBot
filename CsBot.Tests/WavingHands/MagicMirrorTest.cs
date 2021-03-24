using CsBot.Games.WavingHands;
using CsBot.Games.WavingHands.Living;
using CsBot.Games.WavingHands.Spells;
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

			hand.Add (Gesture.Clap);
			hand.Add (Gesture.Wave);
			hand2.Add (Gesture.Clap);
			hand2.Add (Gesture.Wave);

			Spell magicMirror = new MagicMirror (wizard);

			Assert.True (magicMirror.IsMatch (hand, hand2));
		}
	}
}