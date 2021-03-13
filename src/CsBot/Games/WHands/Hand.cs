namespace CsBot.Games.WHands
{
	public class Hand
	{
		public enum Side { right, left };

		private Side _side;
		private char[] _gestures;

		private const int MaxGestures = 10;

		public Hand (Side s)
		{
			_side = s;
			_gestures = new char[MaxGestures];
		}

		public Hand Add (char toAdd)
		{
			for (int i = 0; i < (MaxGestures-1); i++) {
				_gestures[i] = _gestures[i + 1];
			}

			_gestures[MaxGestures - 1] = toAdd;

			return this;
		}

		public char[] GetLast (int n)
		{
			char[] retVal = new char[n];

			for (int i = 0; i < n; i++) {
				retVal[n - i - 1] = _gestures[MaxGestures - i - 1];
			}

			return retVal;
		}

		public char GetAt (int i)
		{
			return _gestures[i];
		}

		public Side GetSide ()
		{
			return _side;
		}
	}
}