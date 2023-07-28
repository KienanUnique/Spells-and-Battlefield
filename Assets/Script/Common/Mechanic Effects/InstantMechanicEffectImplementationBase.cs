using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;

namespace Common.Mechanic_Effects
{
    public abstract class InstantMechanicEffectImplementationBase : SpellImplementationBase, IMechanicEffect
    {
        public abstract void ApplyEffectToTarget(IInteractable target);

        public virtual void ApplyEffectToTargets(IReadOnlyCollection<IInteractable> targets)
        {
            foreach (var interactableTarget in targets)
            {
                ApplyEffectToTarget(interactableTarget);
            }
        }
    }
}