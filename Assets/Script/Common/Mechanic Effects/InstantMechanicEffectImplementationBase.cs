using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Source;

namespace Common.Mechanic_Effects
{
    public abstract class InstantMechanicEffectImplementationBase : IMechanicEffect
    {
        public abstract void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation);

        public virtual void ApplyEffectToTargets(IEnumerable<IInteractable> targets,
            IEffectSourceInformation sourceInformation)
        {
            foreach (IInteractable interactableTarget in targets)
            {
                ApplyEffectToTarget(interactableTarget, sourceInformation);
            }
        }
    }
}