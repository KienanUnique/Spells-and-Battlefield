using System;
using Interfaces;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.Spells_With_Cooldown_Selector
{
    public interface ISpellWithCooldown
    {
        bool CanUse { get; }
        event Action CanUseAgain;
        void SetCoroutineStarter(ICoroutineStarter coroutineStarter);
        ISpell GetSpellAndStartCooldownTimer();
    }
}