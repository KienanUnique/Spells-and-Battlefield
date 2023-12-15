using System;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public interface IReadonlySpellSelector
    {
        public event Action CanUseSpellsAgain;
        public bool CanUseSpell { get; }
    }
}