namespace CsBot.Games.WavingHands.Spells.Damaging
{
	public class Fireball : Spell
	{
		public Fireball (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.WigglingFingers, Gesture.Snap, Gesture.Snap, Gesture.DigitPoint, Gesture.DigitPoint
			};

			Target = target;

			Description = "This has the same effect as cause light wounds but inflicts three points of damage instead ";
			Description += "of two.";

			Usage = "Fireball can be cast with Gestures: ";
			Usage += "Wiggling Fingers (F), Snap (S), Snap (S), Digit Point (D) and DigitPoint(D)";
		}
	}
}