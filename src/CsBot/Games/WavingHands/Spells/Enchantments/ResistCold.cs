namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class ResistCold : Enchantment
	{
		public ResistCold (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Snap, Gesture.Snap, Gesture.WigglingFingers, Gesture.ProfferedPalm};

			Target = target;

			Description = "The effects of this spell are identical to resist heat but resistance is to cold ";
			Description += "(ice storm and ice elementals) and it destroys ice elementals if they are the subject ";
			Description += "of the spell but doesn't affect fire elementals. ";

			Usage = "Resist Cold can be cast with Gestures: ";
			Usage += "Snap (S), Snap (S), Wiggling Fingers (F) and Proffered Palm (P)";
		}
	}
}