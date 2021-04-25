namespace CsBot.Games.WavingHands.Spells
{
	public abstract class Spell
	{
		protected Living.Living Target;
		protected Gesture[] Sequence;
		protected Gesture[] SecondHandSequence; // Not always Used
		protected Gesture[] OtherSequence; // Not always Used
		protected Gesture[] SecondHandOtherSequence; // Not always Used
		protected string Description;
		protected string Usage;
		protected int OtherSequenceLimit = 0, UsedOtherSequence = 0;
		public bool IsMatch (Hand left, Hand right)
		{
			bool isMatch = false;
			bool leftMatch = (IsEqual(left.GetLast (Sequence.Length),Sequence));
			bool rightMatch = (IsEqual(right.GetLast (Sequence.Length),Sequence));
			bool leftMatchOther = false, rightMatchOther = false;

			if ((leftMatch || rightMatch) && !IsNullOrEmpty (SecondHandSequence)) {
				bool secondLeftMatch = (EqualEnough(left.GetLast (SecondHandSequence.Length),SecondHandSequence));
				bool secondRightMatch = (EqualEnough(right.GetLast (SecondHandSequence.Length),SecondHandSequence));

				isMatch = (leftMatch && secondRightMatch) ||
				          (rightMatch && secondLeftMatch);
			} else {
				isMatch = leftMatch || rightMatch;
			}

			if (!isMatch && !IsNullOrEmpty (OtherSequence) &&
				(OtherSequenceLimit < 1 || UsedOtherSequence < OtherSequenceLimit)) {

				UsedOtherSequence++;

				leftMatchOther = (IsEqual(left.GetLast (OtherSequence.Length),OtherSequence));
				rightMatchOther = (IsEqual(right.GetLast (OtherSequence.Length),OtherSequence));

				isMatch = leftMatchOther || rightMatchOther;
			}

			if ((leftMatchOther || rightMatchOther) && !IsNullOrEmpty (SecondHandOtherSequence)) {
				bool secondLeftMatch = (EqualEnough(left.GetLast (SecondHandOtherSequence.Length),SecondHandOtherSequence));
				bool secondRightMatch = (EqualEnough(right.GetLast (SecondHandOtherSequence.Length),SecondHandOtherSequence));

				isMatch = (leftMatchOther && secondRightMatch) ||
				          (rightMatchOther && secondLeftMatch);
			}

			return isMatch;
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

		private bool IsNullOrEmpty (Gesture[] secondHandSequence)
		{
			return secondHandSequence == null || secondHandSequence.Length < 1;
		}

		private bool EqualEnough (Gesture[] first, Gesture[] second)
		{
			if (first.Length != second.Length) {
				return false;
			}

			for (int i = 0; i < first.Length; i++) {
				Gesture g1 = first[i];
				Gesture g2 = second[i];

				if (g1 != g2 && g2 != Gesture.Null) {
					return false;
				}
			}

			return true;
		}
	}
}