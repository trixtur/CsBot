namespace CsBot.Games.WavingHands
{
	public enum Gesture : uint
	{
		Blank		= ' ',
		Null		= '\0',
		ProfferedPalm	= 'P',
		WigglingFingers = 'F',
		Snap		= 'S',
		Wave		= 'W',
		DigitPoint	= 'D',
		Clap		= 'C', // Requires both hands
	}
}