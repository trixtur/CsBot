
namespace CsBot.Games.WavingHands
{
	public class Hand
	{
		public enum Side { Right, Left };

		Side _side;
		Gesture[] _gestures;

		const int MaxGestures = 10;

		public Hand (Side side)
		{
			_side = side;
			_gestures = new Gesture[MaxGestures];
		}

		public Hand Add (Gesture toAdd)
		{
			for (int i = 0; i < (MaxGestures-1); i++) {
				_gestures[i] = _gestures[i + 1];
			}

			_gestures[MaxGestures - 1] = toAdd;

			return this;
		}

		public Gesture[] GetLast (int n)
		{
			Gesture[] retVal = new Gesture[n];

			for (int i = 0; i < n; i++) {
				retVal[n - i - 1] = _gestures[MaxGestures - i - 1];
			}

			return retVal;
		}

		public Gesture GetAt (int i)
		{
			return _gestures[i];
		}

		public Side GetSide ()
		{
			return _side;
		}
	}
}
