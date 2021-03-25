using CsBot.Games.WavingHands.Spells.Enchantments;

namespace CsBot.Games.WavingHands.Living
{
	public abstract class Living
	{
		protected int HP;

		protected Enchantment[] Enchantments { get; set; }
	}
}