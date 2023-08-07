using System;
using Common.Abstract_Bases.Disableable;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public abstract class SpellSelectorBase : BaseWithDisabling, ISpellSelector
    {
        public abstract bool CanUseSpell { get; }
        public event Action CanUseSpellsAgain;
        public abstract ISpell Pop();

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