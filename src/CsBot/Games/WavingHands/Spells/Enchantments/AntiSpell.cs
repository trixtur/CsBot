namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class AntiSpell : Spell
	{
		public AntiSpell (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Snap, Gesture.ProfferedPalm, Gesture.WigglingFingers};

			Target = target;

			Description = "On the turn following the casting of this spell, the subject cannot include any gestures ";
			Description += "made on or before this turn in a spell sequence and must restart a new spell from the ";
			Description += "beginning of that spell sequence. The spell does not affect spells which are cast on the ";
			Description += "same turn nor does it affect monsters.";

			Usage = "Anti-Spell can be cast with Gestures: ";
			Usage += "Snap (S), Proffered Palm (P) and Wiggling Fingers (F)";
		}
	}
}