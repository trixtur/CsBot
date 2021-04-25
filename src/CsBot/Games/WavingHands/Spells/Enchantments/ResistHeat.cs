namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class ResistHeat : Enchantment
	{
		public ResistHeat (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Wave, Gesture.Wave, Gesture.WigglingFingers, Gesture.ProfferedPalm};

			Target = target;

			Description = "The subject of this spell becomes totally resistant to all forms of heat attack ";
			Description += "(fireball, fire storm and fire elementals). Only dispel magic or remove enchantment ";
			Description += "will terminate this resistance once started (although a counter-spell will prevent it ";
			Description += "from working if cast at the subject at the same time as this spell). A resist heat cast ";
			Description += "directly on a fire elemental will destroy it before it can attack that turn, but there ";
			Description += "is no effect on ice elementals.";

			Usage = "Resist Heat can be cast with Gestures: ";
			Usage += "Wave (W), Wave (W), Wiggling Fingers (F) and Proffered Palm (P)";
		}
	}
}