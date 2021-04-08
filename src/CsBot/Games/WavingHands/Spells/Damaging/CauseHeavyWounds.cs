namespace CsBot.Games.WavingHands.Spells.Damaging
{
	public class CauseHeavyWounds : Spell
	{
		public CauseHeavyWounds (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Wave, Gesture.ProfferedPalm, Gesture.WigglingFingers, Gesture.DigitPoint};

			Target = target;

			Description = "This has the same effect as cause light wounds but inflicts three points of damage instead ";
			Description += "of two.";

			Usage = "Cause Heavy Wounds can be cast with Gestures: ";
			Usage += "Wave (W), Proffered Palm (P), Wiggling Fingers (F) and Digit Point (D)";
		}
	}
}