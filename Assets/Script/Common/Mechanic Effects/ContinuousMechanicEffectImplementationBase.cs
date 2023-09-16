using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Continuous_Effect;
using Spells.Abstract_Types.Implementation_Bases;

namespace Common.Mechanic_Effects
{
    public abstract class ContinuousMechanicEffectImplementationBase : SpellImplementationBase, IMechanicEffect
    {
        private readonly IContinuousEffect _effect;

        protected ContinuousMechanicEffectImplementationBase(IContinuousEffect effect)
        {
            _effect = effect;
        }

        public virtual void ApplyEffectToTarget(IInteractable target)
        {
            if (target is IContinuousEffectApplicable continuousEffectApplicableTarget)
            {
                _effect.SetTarget(target);
                continuousEffectApplicableTarget.ApplyContinuousEffect(_effect);
            }
        }

        public virtual void ApplyEffectToTargets(IReadOnlyCollection<IInteractable> targets)
        {
            foreach (IInteractable target in targets)
            {
                ApplyEffectToTarget(target);
            }
        }
    }
}