using Spells.Spell;

namespace Player.Spell_Manager.Spells_Selector
{
    public interface IPlayerSpellsSelectorForSpellManager : IPlayerSpellsSelector
    {
        public ISpell RememberedSpell { get; }
        public bool TryToRememberSelectedSpellInformation();
        public void RemoveRememberedSpell();
    }
}