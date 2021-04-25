namespace CsBot.Games.WavingHands.Spells.Damaging
{
	public class LightningBolt : Spell
	{
		public LightningBolt (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.DigitPoint, Gesture.WigglingFingers, Gesture.WigglingFingers, Gesture.DigitPoint,
				Gesture.DigitPoint
			};

			OtherSequenceLimit = 1;

			OtherSequence = new Gesture[] {Gesture.Wave, Gesture.DigitPoint, Gesture.DigitPoint, Gesture.Clap};
			SecondHandOtherSequence = new Gesture[] {Gesture.Null, Gesture.Null, Gesture.Null, Gesture.Clap};

			Target = target;

			Description = "The subject of this spell is hit by a bolt of lightning and sustains five points of ";
			Description += "damage. Resistance to heat or cold is irrelevant. There are two gesture combinations for ";
			Description += "the spell, but the shorter one may be used only once per day (i.e. per battle) by any ";
			Description += "wizard. The longer one may be used without restriction. A shield spell offers no defence.";

			Usage = "Lightning Bolt can be cast with Gestures: ";
			Usage += "DigitPoint (D), WigglingFingers (F), WigglingFingers (F), DigitPoint (D) and DigitPoint (D) or ";
			Usage += "Wave (W), DigitPoint (D), DigitPoint (D) and Clap(C) but the shorter sequence can only ";
			Usage += "be used once";
		}
	}
}
