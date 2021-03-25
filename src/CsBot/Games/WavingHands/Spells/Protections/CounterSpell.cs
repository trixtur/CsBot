namespace CsBot.Games.WavingHands.Spells.Protections
{
	public class CounterSpell : Spell
	{
		public CounterSpell (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Wave, Gesture.ProfferedPalm, Gesture.ProfferedPalm};
			OtherSequence = new Gesture[] {Gesture.Wave, Gesture.Wave, Gesture.Snap};

			Target = target;

			Description =  "Any other spell cast upon the subject in the same turn has no effect. ";
			Description += "In the case of blanket spells, which affect more than one person, ";
			Description += "the subject of the counter-spell alone is protected.\n";
			Description += "\nFor example, a fire storm spell could kill off a monster but ";
			Description += "not if a counter-spell were cast on the monster in the same turn. ";
			Description += "Everyone else would be affected as usual by the fire storm unless ";
			Description += "they had their own protection. The counter-spell will cancel all the ";
			Description += "spells cast at the subject for that turn (including remove enchantment and ";
			Description += "magic mirror) except dispel magic and finger of death. It will combine with another ";
			Description += "spell of its own type for the same effect as if it were alone. The counter-spell will ";
			Description += "also act as a shield on the final gesture in addition to its other properties, but the ";
			Description += "shield effect is on the same subject as its other effect.";

			Usage = "Counter-Spell can be cast with Gestures: ";
			Usage += "Wave (W), Proffered Palm (P) and Proffered Palm (P) or ";
			Usage += "Wave (W), Wave (W) and Snap (S).";
		}
	}
}