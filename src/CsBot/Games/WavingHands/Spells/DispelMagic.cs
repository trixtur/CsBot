namespace CsBot.Games.WavingHands.Spells
{
	public class DispelMagic : Spell
	{
		public DispelMagic (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Clap, Gesture.DigitPoint, Gesture.ProfferedPalm, Gesture.Wave};
			SecondHandSequence = new Gesture[] {Gesture.Clap, Gesture.Null, Gesture.Null, Gesture.Null};

			Target = target;

			Description =  "This spell acts as a combination of counter-spell and remove enchantment, but its effects ";
			Description += "are universal rather than limited to the subject of the spell. It will stop any spell ";
			Description += "cast in the same turn from working (apart from another dispel magic spell which combines ";
			Description += "with it for the same result), and will remove all enchantments from all beings before ";
			Description += "they have effect. In addition all monsters are destroyed although they can attack that ";
			Description += "turn. Counter-spells and magic mirrors have no effect. The spell will not work on stabs ";
			Description += "or surrenders. As with counter-spell it also acts as a shield for the subject.";

			Usage = "Dispel Magic can be cast the Gestures: ";
			Usage += "Clap (C), Digit-Point (D), Proffered Palm (P) and Wave (W)";
		}

	}
}