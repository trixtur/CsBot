namespace CsBot.Games.WavingHands
{
	public enum Gesture : uint
	{
		/// <summary>Blank represents a space</summary>
		Blank		= ' ',
		/// <summary>Null (\0)</summary>
		Null		= '\0',
		/// <summary>ProfferedPam (P)</summary>
		ProfferedPalm	= 'P',
		/// <summary>WigglingFingers (F)</summary>
		WigglingFingers = 'F',
		/// <summary>Snap (S)</summary>
		Snap		= 'S',
		/// <summary>Wave (W)</summary>
		Wave		= 'W',
		/// <summary>DigitPoint (D)</summary>
		DigitPoint	= 'D',
		/// <summary>Clap (C)</summary>
		Clap		= 'C', // Requires both hands
	}
}