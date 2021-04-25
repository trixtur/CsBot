namespace CsBot.Games.WavingHands.Spells.Protections
{
	public class RaiseDead : Spell
	{
		public RaiseDead (Living.Living target)
		{
			Sequence = new Gesture[] {
				Gesture.DigitPoint, Gesture.Wave, Gesture.Wave, Gesture.WigglingFingers, Gesture.Wave, Gesture.Clap
			};
			SecondHandSequence = new Gesture[] {
				Gesture.Null, Gesture.Null, Gesture.Null, Gesture.Null, Gesture.Clap
			};

			Target = target;

			Description =  "The subject of this spell is usually a recently-dead (not yet decomposing) human corpse ";
			Description += "though it may be used on a dead monster. When the spell is cast, life returns to the ";
			Description += "corpse and all damage is cured. All enchantments are also removed (as per the spell) so ";
			Description += "any diseases or poisons will be neutralized and all other enchantments removed. If swords,";
			Description += " knives, or other implements of destruction still remain in the corpse when it is raised, ";
			Description += "they will of course cause it damage as usual. The subject will be able to act normally ";
			Description += "immediately after the spell is cast. On the turn a monster is raised it may attack. ";
			Description += "Wizards may being gesturing on the turn following their return from the dead.\n";
			Description += "\nThis is the only spell which affects corpses. It therefore cannot be stopped by a ";
			Description += "counter-spell. A dispel magic spell will prevent its effect, since dispel magic affects ";
			Description += "all spells no matter what their subject.\n";
			Description += "\nIf the spell is cast on a live individual, the effect is that of the cure light wounds ";
			Description += "recovering five points of damage, or as many as have been sustained if less than five. ";
			Description += "Note that any diseases the live subject might have are not cured.";

			Usage = "Raise Dead can be cast with Gestures: ";
			Usage += "DigitPoint (D), Wave (W), Wave (W), WiggleFingers (F), Wave (W) and Clap (C)";
		}

	}
}