namespace CsBot.Games.WavingHands.Spells.Summoning
{
	public class Elemental : Spell
	{
		public Elemental (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Clap, Gesture.Snap, Gesture.Wave, Gesture.Wave, Gesture.Snap};
			SecondHandSequence = new Gesture[] {Gesture.Clap, Gesture.Null, Gesture.Null, Gesture.Null, Gesture.Null};

			Target = target;

			Description = "This spell creates either a fire elemental or an ice elemental at the discretion of the ";
			Description += "person upon whom the spell is cast after he has seen all the gestures made that turn. ";
			Description += "Elementals must be cast at someone and cannot be \"shot off\" harmlessly at some ";
			Description += "inanimate object.\n";

			Description += "\nThe elemental will, for that turn and until destroyed, attack everyone who is not ";
			Description += "resistant to its type (heat or cold), causing three points of damage per turn. The ";
			Description += "elemental takes three points of damage to be killed but may be destroyed by spells of ";
			Description += "the opposite type (e.g. fire storm, resist cold or fireball will kill an ice elemental), ";
			Description += "and will also neutralize the canceling spell. Elementals will not attack on the turn ";
			Description += "they are destroyed by such a spell. An elemental will also be engulfed and destroyed by ";
			Description += "a storm of its own type but, in such an event, the storm is not neutralized although the ";
			Description += "elemental still does not attack in that turn. Two elementals of the opposite type will ";
			Description += "also destroy each other before attacking, and two of the same type will join together to ";
			Description += "form a single elemental of normal strength. Note that only wizards or monsters resistant ";
			Description += "to the type of elemental, or who are casting a spell which has the effect of a shield do ";
			Description += "not get attacked by the elemental. Casting a fireball upon yourself when being attacked ";
			Description += "by an ice elemental is no defence! (Cast it at the elemental...)";

			Usage = "Summon Elemental can be case with Gestures: ";
			Usage += "Clap (C Both Hands), Snap (S), Wave (W), Wave (W) and Snap (S)";
		;
	}
	}
}