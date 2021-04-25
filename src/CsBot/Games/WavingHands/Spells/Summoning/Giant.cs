namespace CsBot.Games.WavingHands.Spells.Summoning
{
	public class Giant : Spell
	{
		public Giant (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.Wave, Gesture.WigglingFingers, Gesture.ProfferedPalm, Gesture.Snap, Gesture.WigglingFingers,
				Gesture.Wave
			};

			Target = target;

			Description =  "This spell is the same as summon goblin but the giant created inflicts and is destroyed ";
			Description += "by four points of damage rather than one.";

			Usage = "Summon Giant can be cast with Gestures: ";
			Usage += "Wave (W), WigglingFingers (F), ProfferedPalm (P) ";
			Usage += "Snap (S), WigglingFingers (F) and Wave (W)";
		}
	}
}