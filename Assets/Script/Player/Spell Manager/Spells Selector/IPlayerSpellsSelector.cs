using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;

namespace Player.Spell_Manager.Spells_Selector
{
    public interface IPlayerSpellsSelector : IPlayerSpellsSelectorInformation
    {
        public void SelectSpellTypeWithIndex(int indexToSelect);
        public void SelectNextSpellType();
        public void SelectPreviousSpellType();
        public void AddSpell(ISpell newSpell);
        public void AddSpell(ISpellType spellType, ISpell newSpell);
    }
}