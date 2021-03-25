namespace CsBot.Games.WavingHands.Spells.Summoning
{
	public class Goblin : Spell
	{
		public Goblin (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.Snap, Gesture.WigglingFingers, Gesture.Wave};

			Target = target;

			Description =  "This spell creates a goblin under the control of the target upon whom the spell is cast ";
			Description += "(or if cast on a monster, the target monster's controller, even if the monster later dies ";
			Description += "or changes loyalty). The goblin can attack immediately and its victim can be any wizard ";
			Description += "or other monster the controller desires, stating which at the time they write their ";
			Description += "gestures. It does one point of damage to its victim per turn and is destroyed after one ";
			Description += "point of damage is inflicted upon it.\n";

			Description += "The summoning spell cannot be cast at an elemental, and if cast at something which ";
			Description +="doesn't exist, the spell has no effect.";

			Usage = "Summon Goblin can be cast with Gestures: ";
			Usage += "Snap (S), WiggleFingers (F) and Wave (W)";
		}
	}
}