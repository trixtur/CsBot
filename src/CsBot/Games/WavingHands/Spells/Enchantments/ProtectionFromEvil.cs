namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class ProtectionFromEvil : Enchantment
	{
		public ProtectionFromEvil (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Wave, Gesture.Wave, Gesture.ProfferedPalm};

			Target = target;

			Description = "For this turn and the following 3 turns the subject of this spell is protected as if ";
			Description += "using a shield spell, thus leaving both hands free. Concurrent shield spells offer no ";
			Description += "further protection and compound protection from evil spells merely overlap offering no ";
			Description += "extra cover.";

			Usage = "Protection from Evil can be cast with Gestures: ";
			Usage += "Wave (W), Wave (W) and Proffered Palm (P)";
		}
	}
}