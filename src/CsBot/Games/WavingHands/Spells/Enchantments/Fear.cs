namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class Fear : Enchantment
	{
		public Fear (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Snap, Gesture.Wave, Gesture.DigitPoint};

			Target = target;

			Description = "In the turn following the casting of this spell, the subject cannot perform a C, D, F or ";
			Description += "S gesture. This obviously has no effect on monsters. If the subject is also the subject ";
			Description += "of amnesia, confusion, charm person, charm monster or paralysis, then none of the ";
			Description += "spells work.";

			Usage = "Fear can be cast with Gestures: ";
			Usage += "Snap (S), Wave (W) and Digit Point (D)";
		}

	}
}