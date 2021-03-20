namespace CsBot.Games.WavingHands.Spells
{
	public class Shield : Spell
	{
		public Shield (Living.Living target)
		{
			Sequence = new Gesture[]{Gesture.ProfferedPalm};

			Target = target;

			Description =  "This spell protects the subject from monsters (that is, creatures created by a ";
			Description += "summoning spell), from missile spells and from stabs by wizards. The shield ";
			Description += "will block any number of such attacks but lasts for only one round. ";
			Description += "The shield protects the subject on the turn in which it was cast.";

			Usage = "Shield can be case with Gestures: Proffered Palm (P)";
		}
	}
}