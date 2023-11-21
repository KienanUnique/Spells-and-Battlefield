using Spells.Spell;

namespace Common.Abstract_Bases.Spells_Manager
{
    public interface ISpellSelectorFoSpellManager
    {
        public ISpell RememberedSpell { get; }
        public bool TryToRememberSelectedSpellInformation();
        public void RemoveRememberedSpell();
    }
}