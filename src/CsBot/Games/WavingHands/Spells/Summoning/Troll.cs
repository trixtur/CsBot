namespace CsBot.Games.WavingHands.Spells.Summoning
{
	public class Troll : Spell
	{
		public Troll (Living.Living target)
		{
				Sequence = new Gesture[] {
					Gesture.WigglingFingers, Gesture.ProfferedPalm, Gesture.Snap, Gesture.WigglingFingers, Gesture.Wave
				};

				Target = target;

				Description =  "This spell is the same as summon goblin but the troll created inflicts and is ";
				Description += "destroyed by three points of damage rather than one. ";

				Usage = "Summon Troll can be cast with Gestures: ";
				Usage += "WigglingFingers (F), ProfferedPam (P), Snap (S), WigglingFingers (F) and Wave (W)";
		}
	}
}