namespace CsBot.Games.WavingHands.Spells
{
	public class Missile : Spell
	{
		public Missile (Living.Living target)
		{
			Sequence = new Gesture[]{Gesture.Snap,Gesture.DigitPoint};

			Target = target;

			Description =  "This spell creates a material object of hard substance which is hurled ";
			Description += "towards the subject of the spell and causes them one point of damage. The ";
			Description += "spell is thwarted by a shield in addition to the usual count-spell, ";
			Description += "dispel magic and magic mirror ( the latter causing it to hit whoever ";
			Description += "cast it instead).";

			Usage = "Missile can be cast with Gestures: Snap (S) and Digit Point (D)";
		}
	}
}