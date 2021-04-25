namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class CharmPerson : Enchantment
	{
		public CharmPerson (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.ProfferedPalm, Gesture.Snap, Gesture.DigitPoint, Gesture.WigglingFingers};

			Target = target;

			Description = "Except for cancellation with other enchantments, this spell only affects humans. The ";
			Description += "subject is told which of his hands will be controlled at the time the spell hits, and in ";
			Description += "the following turn, the caster of the spell writes down the gesture he wants the ";
			Description += "subject's named hand to perform. This could be a stab or nothing. If the subject is only ";
			Description += "so because of a reflection from a magic mirror the subject of the mirror assumes the role ";
			Description += "of caster and writes down his opponent's gesture. If the subject is also the subject of ";
			Description += "any of amnesia, confusion, charm monster, paralysis or fear, none of the spells work.";

			Usage = "Charm Person can be cast with Gestures: ";
			Usage += "Proffered Palm (P), Snap (S), DigitPoint (D) and WigglingFingers (F)";
		}
	}
}