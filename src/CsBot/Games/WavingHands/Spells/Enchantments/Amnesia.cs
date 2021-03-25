namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class Amnesia : Enchantment
	{
		public Amnesia (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.DigitPoint, Gesture.ProfferedPalm, Gesture.ProfferedPalm};

			Target = target;

			Description =  "If the subject opf this spell is a wizard, next turn they must repeat identically the ";
			Description += "gestures they made in the current turn, including stabs. If the subject is a monster it ";
			Description += "will attack whoever it attacked in this turn. If the subject is simultaneously the ";
			Description += "subject of any of confusion, charm person, charm monster, paralysis or fear then none ";
			Description += "of the spells work.";

			Usage = "Amnesia can be cast with Gestures: ";
			Usage += "DigitPoint (D), ProfferedPalm (P) and ProfferedPalm (P)";
		}
	}
}