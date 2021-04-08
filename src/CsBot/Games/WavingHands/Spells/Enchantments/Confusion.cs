using CsBot.Games.WavingHands.Spells.Damaging;

namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class Confusion : Enchantment
	{
		public Confusion (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.DigitPoint, Gesture.Snap, Gesture.WigglingFingers};

			Target = target;

			Description = "If the subject of this spell is a wizard, next turn he writes down his gestures as usual ";
			Description += "and after exposing them, rolls 2 dice to determine which gesture is superseded due to ";
			Description += "his being confused. The first die indicates left hand on 1-3, right on 4-6. The second ";
			Description += "roll determines what the gesture for that hand shall be replaced with: 1=C, 2=D, 3=F, ";
			Description += "4=P, 5=S, 6=W. If the subject of the spell is a monster, it attacks at random that turn. ";
			Description += "If the subject is also the subject of any of: amnesia, charm person, charm monster, ";
			Description += "paralysis or fear, none of the spells work.";

			Usage = "Confusion can be cast with Gestures : ";
			Usage += "Digit Point (D), Snap (S) and Wiggling Fingers (F)";
		}

	}
}