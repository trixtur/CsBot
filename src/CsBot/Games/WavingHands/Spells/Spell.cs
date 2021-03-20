namespace CsBot.Games.WavingHands.Spells
{
	public abstract class Spell
	{
		protected Living.Living Target;
		protected Gesture[] Sequence;
		protected string Description;
		protected string Usage;
		public bool IsMatch (Hand left, Hand right)
		{
			return (IsEqual(left.GetLast (Sequence.Length),Sequence) ||
			        IsEqual(right.GetLast (Sequence.Length),Sequence));
		}

		public string GetDescription ()
		{
			return Description;
		}

		public string GetUsage ()
		{
			return Usage;
		}

		private bool IsEqual (Gesture[] first, Gesture[] second)
		{
			if (first.Length != second.Length) {
				return false;
			}

			for (int i = 0; i < first.Length; i++) {
				Gesture g1 = first[i];
				Gesture g2 = second[i];

				if (g1 != g2) {
					return false;
				}
			}

			return true;
		}
	}
}