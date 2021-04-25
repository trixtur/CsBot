namespace CsBot.Games.WavingHands.Spells
{
	public class Stab : Spell
	{
		public Stab (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Stab};

			Target = target;

			Description = "This is not a spell but an attack which can be directed at any individual monster or ";
			Description += "wizard. Unless protected in that turn by a shield spell or another spell with the same ";
			Description += "effect, the being stabbed suffers 1 point of damage. The wizard only has one knife so ";
			Description += "can only stab with one hand in any turn, although which hand doesn't matter. The stab ";
			Description += "cannot be reflected by a magic mirror or stopped by dispel magic (although its shield ";
			Description += "effect *could* stop the stab). Wizards are not allowed to stab themselves and must choose ";
			Description += "a target for the stab. Knives cannot be thrown.";

			Usage = "Stab is a single handed gesture: stab";
		}
	}
}