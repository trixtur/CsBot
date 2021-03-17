namespace CsBot.Games.WHands
{
	public class Wizard
	{
		private Hand[] _hands;
		private int HP;

		public Wizard ()
		{
			_hands = new Hand[2];
			_hands[0] = new Hand (Hand.Side.right);
			_hands[1] = new Hand (Hand.Side.left);

			HP = 15;
		}
	}
}