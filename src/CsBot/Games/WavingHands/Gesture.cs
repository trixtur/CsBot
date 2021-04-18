using System;
using System.Runtime.CompilerServices;

namespace CsBot.Games.WavingHands
{
	public static class Extensions
	{
		public static Gesture current = Gesture.Null;
		public static Gesture GetGesture(this char c)
		{
			Gesture gesture = current;

			switch (Char.ToLower (c)) {
				case 'p':
					gesture = Gesture.ProfferedPalm;
					break;
				case 'f':
					gesture = Gesture.WigglingFingers;
					break;
				case 's':
					gesture = Gesture.Snap;
					break;
				case 'w':
					gesture = Gesture.Wave;
					break;
				case 'd':
					gesture = Gesture.DigitPoint;
					break;
				case 'c':
					gesture = Gesture.Clap;
					break;
				default:
					gesture = current;
					break;
			}

			return gesture;
		}
	}

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