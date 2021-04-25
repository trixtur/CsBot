namespace CsBot.Games.WavingHands.Spells.Summoning
{
	public class Ogre : Spell
	{
		public Ogre (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.ProfferedPalm, Gesture.Snap, Gesture.WigglingFingers, Gesture.Wave};

			Target = target;

			Description =  "This spell is the same as summon goblin but the ogre created inflicts and is destroyed by ";
			Description += "two points of damage rather than one.";

			Usage = "Summon Ogre can be cast with Gestures: ";
			Usage += "ProfferedPalm (P), Snap (S), WiggingFingers (F) and Wave (W)";
		}

	}
}