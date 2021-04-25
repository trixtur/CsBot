namespace CsBot.Games.WavingHands.Spells.Damaging
{
	public class CauseLightWounds : Spell
	{
		public CauseLightWounds (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Wave, Gesture.WigglingFingers, Gesture.ProfferedPalm};

			Target = target;

			Description  = "The subject of this spell is inflicted with two points of damage. Resistance to heat ";
			Description += "or cold offers no defence. A simultaneous cure light wounds will serve only to reduce ";
			Description += "the damage to 1 point. A shield has no effect. ";

			Usage = "Cause Light Wounds can be cast with Gestures: ";
			Usage += "Wave (W), Wiggling Fingers (F) and Proffered Palm (P)";
		}
	}
}