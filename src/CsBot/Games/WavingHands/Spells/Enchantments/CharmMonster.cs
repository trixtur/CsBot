namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class CharmMonster : Enchantment
	{
		public CharmMonster (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.ProfferedPalm, Gesture.Snap, Gesture.DigitPoint, Gesture.DigitPoint};

			Target = target;

			Description = "Except for cancellation with other enchantments, this spell only affects monsters ";
			Description += "(excluding elementals). Control of the monster is transferred to the caster of the ";
			Description += "spell (or retained by him) as of this turn, i.e. the monster will attack whosoever its ";
			Description += "new controller dictates from that turn onwards including that turn. Further charms are, ";
			Description += "of course, possible, transferring as before. If the subject of the charm is also the ";
			Description += "subject of any of: amnesia, confusion, charm person, fear or paralysis, none of the ";
			Description += "spells work.";

			Usage = "Charm Monster can be cast with Gestures: ";
			Usage += "Proffered Palm (P), Snap (S), Digit Point (D) and Digit Point (D)";
		}
	}
}