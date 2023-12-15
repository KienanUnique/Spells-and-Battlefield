using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    public interface ISpellSelectorFoSpellManager
    {
        public ISpell RememberedSpell { get; }
        public bool TryToRememberSelectedSpellInformation();
        public void RemoveRememberedSpell();
    }
}