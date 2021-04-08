namespace CsBot.Games.WavingHands.Spells.Damaging
{
	public class FireStorm : Spell
	{
		public FireStorm (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Snap, Gesture.Wave,Gesture.Wave,Gesture.Clap};

			SecondHandSequence = new Gesture[] {Gesture.Null,Gesture.Null,Gesture.Null,Gesture.Clap};

			Target = target;

			Description = "Everything not resistant to heat sustains 5 points of damage that turn. The spell cancels ";
			Description += "wholly, causing no damage, with either an ice storm or an ice elemental. It will destroy ";
			Description += "but not be destroyed by a fire elemental. Two fire storms act as one.";

			Usage = "Fire Storm can be cast with Gestures: ";
			Usage += "Snap (S), Wave (W), Wave (W) and Clap (C Both hands)";
		}
	}
}