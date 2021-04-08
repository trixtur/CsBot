namespace CsBot.Games.WavingHands.Spells.Enchantments
{
	public class Paralysis : Enchantment
	{
		public Paralysis (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.WigglingFingers, Gesture.WigglingFingers, Gesture.WigglingFingers};

			Target = target;

			Description = "If the subject of the spell is a wizard, then on the turn the spell is cast, after ";
			Description += "gestures have been revealed, the caster selects one of the wizard's hands and on the next ";
			Description += "turn that hand is paralyzed into the position it is in this turn. If the wizard already ";
			Description += "had a paralyzed hand, it must be the same hand which is paralyzed again. Certain gestures ";
			Description += "remain the same but if the hand being paralyzed is performing a C, S or W it is instead ";
			Description += "paralyzed into F, D or P respectively, otherwise it will remain in the position written ";
			Description += "down (this allows repeated stabs). A favourite ploy is to continually paralyze a hand ";
			Description += "(F-F-F-F-F-F etc.) into a non-P gesture and then set a monster on the subject so that he ";
			Description += "has to use his other hand to protect himself, but then has no defence against other ";
			Description += "magical attacks. If the subject of the spell is a monster (excluding elementals which ";
			Description += "are unaffected) it simply does not attack in the turn following the one in which the ";
			Description += "spell was cast. If the subject of the spell is also the subject of any of amnesia, ";
			Description += "confusion, charm person, charm monster or fear, none of the spells work.";

			Usage = "Paralysis can be cast with Gestures: ";
			Usage += "WigglingFingers (F), WigglingFingers (F) and WigglingFingers (F)";
		}
	}
}