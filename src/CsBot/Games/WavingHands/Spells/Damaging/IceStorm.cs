namespace CsBot.Games.WavingHands.Spells.Damaging
{
	public class IceStorm : Spell
	{
		public IceStorm (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Wave, Gesture.Snap, Gesture.Snap, Gesture.Clap};

			SecondHandSequence = new Gesture[] {Gesture.Null, Gesture.Null, Gesture.Null, Gesture.Clap};

			Target = target;

			Description =
				"Everything not resistant to cold sustains 5 points of damage that turn. The spell cancels wholly, ";
			Description += "causing no damage, with either a fire storm or a fire elemental, and will cancel locally ";
			Description += "with a fireball. It will destroy but not be destroyed by an ice elemental. Two ice storms ";
			Description += "act as one.";

			Usage = "Ice Storm can be cast with Gestures: ";
			Usage += "Wave (W), Snap (S), Snap (S) Clap (C both hands)";
		}

	}
}