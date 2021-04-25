namespace CsBot.Games.WavingHands.Spells
{
	public class Surrender : Spell
	{
		public Surrender ()
		{
			Sequence = new Gesture[] {Gesture.ProfferedPalm};

			SecondHandSequence = new Gesture[] {Gesture.ProfferedPalm};

			// No Target, This is not a spell.

			Description = "This is not a spell and consequently cannot be cast at anyone. The wizard making these ";
			Description += "gestures, irrespective of whether they terminate spells or not, surrenders and the ";
			Description += "contest is over. The surrendering wizard is deemed to have lost unless his gestures ";
			Description += "completed spells which killed his opponent. Two simultaneous surrenders count as a ";
			Description += "draw. It is a skill for wizards to work their spells so that they never accidentally ";
			Description += "perform 2 P gestures simultaneously. Wizards can be killed as they surrender if hit ";
			Description += "with appropriate spells or attacked physically, but the \"referees\" will cure any ";
			Description += "diseases, poisons etc immediately after the surrender for them. ";

			Usage = "Surrender can be Gestured as: ";
			Usage += "Proffered Palm (P both hands)";
		}
	}
}