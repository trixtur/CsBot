namespace CsBot.Games.WavingHands.Spells.Protections
{
	public class RemoveEnchantment : Spell
	{
		public RemoveEnchantment (Living.Living target)
		{
			Sequence = new Gesture[] {Gesture.ProfferedPalm, Gesture.DigitPoint, Gesture.Wave, Gesture.ProfferedPalm};

			Target = target;

			Description =  "Terminates the effects of all Enchantment Spells ";
			Description += "that have been cast on the subject including those ";
			Description += "that are cast on the subject at the same time ";
			Description += "as the remove enchantment. Effects which have already ";
			Description += "passed are not reversed. For example, the victim of a ";
			Description += "blindness spell would not be able to see what their ";
			Description += "opponent's gestures were when the blindness is removed. ";
			Description += "Note that all enchantments are removed and the caster ";
			Description += "may not pick and choose. Remove enchantment also destroys ";
			Description += "any monster upon which it is cast, although the monster ";
			Description += "can attack in that turn. Wizards suffer no adverse effects from ";
			Description += "this spell, aside from the removal of their enchantments.";

			Usage =  "Remove Enchantment can be cast with Gestures: ";
			Usage += "Proffered Palm (P), Digit Point (D), Wave (W) and Proffered Palm (P)";
		}
	}
}