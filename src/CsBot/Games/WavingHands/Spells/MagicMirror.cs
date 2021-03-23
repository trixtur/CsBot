using System;

namespace CsBot.Games.WavingHands.Spells
{
	public class MagicMirror : Spell
	{
		public MagicMirror (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Clap, Gesture.Wave};
			SecondHandSequence = new Gesture[] {Gesture.Clap, Gesture.Wave};

			Target = target;

			Description = "Any spell cast on a subject protected by this spell is reflected back upon the caster of ";
			Description += "that spell. The magic mirror protects only during the turn in which it is cast. The ";
			Description += "protection includes spells like missile and lightning bolt but does not include attacks ";
			Description += "by monsters or stabs from wizards.\n";
			Description += "\nThe mirror does not affect spells which are cast upon oneself (e.g. spells from this ";
			Description += "section and the Summoning Section). The mirror is countered totally if either a ";
			Description += "counter-spell or dispel magic are cast on the subject in the same turn as the mirror. The ";
			Description += "mirror has no effect on spells which affect more than one person (such as fire storm). To ";
			Description += "mirrors cast at one subject simultaneously combine to form a single mirror.";

			Usage  = "Magic Mirror can be cast with Gestures: ";
			Usage += "Clap (C) and Wave (W (both hands))";
		}
	}
}