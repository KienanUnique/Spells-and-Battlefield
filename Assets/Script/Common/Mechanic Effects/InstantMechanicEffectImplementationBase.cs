using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;

namespace Common.Mechanic_Effects
{
    public abstract class InstantMechanicEffectImplementationBase : SpellImplementationBase, IMechanicEffect
    {
        public abstract void ApplyEffectToTarget(IInteractable target);

        public virtual void ApplyEffectToTargets(List<IInteractable> targets)
        {
            targets.ForEach(target => ApplyEffectToTarget(target));
        }
    }
}