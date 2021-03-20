namespace CsBot.Games.WavingHands.Spells
{
	public abstract class Spell
	{
		protected Living.Living Target;
		protected Gesture[] Sequence;
		public abstract bool IsMatch (Hand left, Hand right);
	}
}