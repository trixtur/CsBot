using System;

namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class Disease : Enchantment
	{
		public Disease (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.DigitPoint, Gesture.Snap, Gesture.WigglingFingers, Gesture.WigglingFingers,
				Gesture.WigglingFingers, Gesture.Clap
			};

			SecondHandSequence = new Gesture[] {
				Gesture.Null,
				Gesture.Null,
				Gesture.Null,
				Gesture.Null,
				Gesture.Null,
				Gesture.Clap
			};

			Target = target;

			Description = "The subject of this spell immediately contracts a deadly (non-contagious) disease which ";
			Description += "will kill him at the end of 6 turns counting from the one upon which the spell is cast. ";
			Description += "The malady is cured by remove enchantment or cure heavy wounds or dispel magic in ";
			Description += "the meantime. ";

			Usage = "Disease can be cast with Gestures: ";
			Usage += "Digit Point (P), Snap (S), Wiggling Fingers (W), ";
			Usage += "Wiggling Fingers (W), Wiggling Fingers (W), Clap (C both hands)";
		}
	}
}