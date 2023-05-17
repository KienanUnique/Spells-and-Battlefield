using System.Collections.Generic;
using Interfaces;
using Spells.Continuous_Effect;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellContinuousMechanicEffectImplementationBase : SpellImplementationBase,
        ISpellMechanicEffect
    {
        private readonly IContinuousEffect _effect;

        protected SpellContinuousMechanicEffectImplementationBase(IContinuousEffect effect)
        {
            _effect = effect;
        }

        public virtual void ApplyEffectToTarget(ISpellInteractable target)
        {
            _effect.SetTarget(target);
            target.ApplyContinuousEffect(_effect);
        }

        public virtual void ApplyEffectToTargets(List<ISpellInteractable> targets)
        {
            foreach (var target in targets)
            {
                ApplyEffectToTarget(target);
            }
        }
    }
}