using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Mechanic_Effects.Continuous_Effect.Factory;
using Common.Mechanic_Effects.Source;
using Spells.Abstract_Types.Implementation_Bases;

namespace Common.Mechanic_Effects
{
    public abstract class ContinuousMechanicEffectImplementationBase : SpellImplementationBase, IMechanicEffect
    {
        private readonly IContinuousEffectFactory _effectFactory;

        protected ContinuousMechanicEffectImplementationBase(IContinuousEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
        }

        public virtual void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation)
        {
            if (target is IContinuousEffectApplicable continuousEffectApplicableTarget)
            {
                IContinuousEffect effect = _effectFactory.Create(target);
                continuousEffectApplicableTarget.ApplyContinuousEffect(effect);
            }
        }

        public virtual void ApplyEffectToTargets(IEnumerable<IInteractable> targets,
            IEffectSourceInformation sourceInformation)
        {
            foreach (IInteractable target in targets)
            {
                ApplyEffectToTarget(target, sourceInformation);
            }
        }
    }
}