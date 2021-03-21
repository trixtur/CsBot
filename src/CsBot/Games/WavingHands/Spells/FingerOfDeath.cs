using System.Collections.Generic;

namespace CsBot.Games.WavingHands.Spells
{
	public class FingerOfDeath : Spell
	{
		public FingerOfDeath (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.ProfferedPalm,
				Gesture.Wave,
				Gesture.ProfferedPalm,
				Gesture.WigglingFingers,
				Gesture.Snap,
				Gesture.Snap,
				Gesture.Snap,
				Gesture.DigitPoint
			};

			Target = target;

			Description =  "Kills the subject stone dead. This spell is so ";
			Description += "powerful that it is unaffected by a counter-spell ";
			Description += "although a dispel magic spell cast upon the final gesture ";
			Description += "will stop it. The usual way to prevent being harmed by ";
			Description += "this spell is to disrupt it during casting using, for example, ";
			Description += "an anti-spell.";

			Usage = "Finger of Death can be cast with Gestures: ";
			Usage += "Proffered Palm (P), Wave (W), Proffered Palm (P) ";
			Usage += "Wiggling Fingers (F), Snap (S), Snap (S), Snap (S) ";
			Usage += " and Digit Point (D).";
		}

	}
}