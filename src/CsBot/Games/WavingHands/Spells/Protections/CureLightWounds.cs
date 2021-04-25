namespace CsBot.Games.WavingHands.Spells.Protections
{
	public class CureLightWounds : Spell
	{
		public CureLightWounds (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.DigitPoint, Gesture.WigglingFingers, Gesture.Wave};

			Target = target;

			Description =  "If the subject has received damage then they are cursed by one point as if that point ";
			Description += "had not been inflicted.\n";

			Description += "\nThus, for example, if a wizard was at ten points of damage and was hit simultaneously ";
			Description += "by a cure light wounds and a lightning bolt they would finish that turn on fourteen ";
			Description += "points rather than fifteen (or nine if there had been no lightning bolt).\n";

			Description += "\nThe effect is not removed by a dispel magic or remove enchantment.";

			Usage = "Cure Light Wounds can be cast with Gestures: ";
			Usage += "DigitPoint (D), WiggleFingers (F) and Wave (W)";
		}
	}
}