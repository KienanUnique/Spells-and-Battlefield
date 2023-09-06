using System;
using Common.Abstract_Bases.Disableable;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public interface ISpellSelector : IReadonlySpellSelector, IDisableable
    {
        public ISpell Pop();
    }

    public interface IReadonlySpellSelector
    {
        public event Action CanUseSpellsAgain;
        public bool CanUseSpell { get; }
    }
}