namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class Poison : Enchantment
	{
		public Poison (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.DigitPoint, Gesture.Wave, Gesture.Wave, Gesture.WigglingFingers, Gesture.Wave,
				Gesture.DigitPoint
			};

			Target = target;

			Description = "This is the same as the disease spell except that cure heavy wounds does not stop ";
			Description += "its effects. ";

			Usage = "Poison can be cast with Gestures: ";
			Usage += "Digit Point (D), Wave (W), Wave (W), Wiggling Fingers (F), Wave (W) and Digit Point (D)";
		}
	}
}