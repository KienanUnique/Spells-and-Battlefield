using System;
using Common.Abstract_Bases.Disableable;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public abstract class EnemySpellSelectorBase : BaseWithDisabling, IEnemySpellSelector
    {
        public event Action CanUseSpellsAgain;
        public abstract bool CanUseSpell { get; }
        public abstract ISpell RememberedSpell { get; }
        public abstract bool TryToRememberSelectedSpellInformation();
        public abstract void RemoveRememberedSpell();

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected void InvokeCanUseSpellsAgain()
        {
            CanUseSpellsAgain?.Invoke();
        }
    }
}