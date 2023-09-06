using System;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    public interface ISpellWithCooldown
    {
        public event Action CanUseAgain;
        public bool CanUse { get; }
        public ISpell GetSpellAndStartCooldownTimer();
    }
}