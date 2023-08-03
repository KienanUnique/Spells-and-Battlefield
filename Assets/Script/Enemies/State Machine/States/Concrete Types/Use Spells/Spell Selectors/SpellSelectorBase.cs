using System;
using Common.Abstract_Bases.Disableable;
using Interfaces;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public abstract class SpellSelectorBase : BaseWithDisabling, ISpellSelector
    {
        protected ICoroutineStarter _coroutineStarter;

        public virtual void Initialize(ICoroutineStarter coroutineStarter)
        {
            _coroutineStarter = coroutineStarter;
        }

        public abstract bool CanUseSpell { get; }
        public abstract event Action CanUseSpellsAgain;
        public abstract ISpell Pop();

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}