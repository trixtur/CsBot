
namespace CsBot.Games.WavingHands.Living
{
	public class Wizard : Living
	{
		Hand[] _hands;
		public Wizard ()
		{
			_hands = new Hand[2];
			_hands[0] = new Hand (Hand.Side.Right);
			_hands[1] = new Hand (Hand.Side.Left);

			HP = 15;
		}
	}
}
