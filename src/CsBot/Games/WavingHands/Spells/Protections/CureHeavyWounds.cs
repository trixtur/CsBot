namespace CsBot.Games.WavingHands.Spells.Protections
{
	public class CureHeavyWounds : Spell
	{
		public CureHeavyWounds (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.DigitPoint, Gesture.WigglingFingers, Gesture.ProfferedPalm, Gesture.Wave};

			Target = target;

			Description =  "This spell is similar to cure light wounds in effect but two points of damage are cured. ";
			Description += "Note that only one point is cured if only one point of damage has been sustained and the ";
			Description += "spell has no effect if the subject is completely undamaged.\n";
			Description += "\nThis spell will also cure any diseases the subject might have at the time.";

			Usage = "Cure Heavy Wounds can be cast with Gestures: ";
			Usage += "DigitPoint (D), WiggingFingers (F), ProfferedPalm (P) and Wave (W)";
		}
	}
}