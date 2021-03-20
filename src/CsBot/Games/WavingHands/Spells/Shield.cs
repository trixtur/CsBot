namespace CsBot.Games.WavingHands.Spells
{
	public class Shield : Spell
	{
		public Shield (Living.Living target)
		{
			Target = target;
			Sequence = new Gesture[1];
			Sequence[0] = Gesture.DigitPoint;
		}

		public override bool IsMatch (Hand left, Hand right)
		{
			return (left.GetLast (1) ==  Sequence || right.GetLast (1) == Sequence);
		}
	}
}