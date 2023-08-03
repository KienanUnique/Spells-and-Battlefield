using System;
using Common.Abstract_Bases.Disableable;
using Interfaces;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public interface ISpellSelector : IDisableable
    {
        void Initialize(ICoroutineStarter coroutineStarter);
        bool CanUseSpell { get; }
        event Action CanUseSpellsAgain;
        ISpell Pop();
    }
}