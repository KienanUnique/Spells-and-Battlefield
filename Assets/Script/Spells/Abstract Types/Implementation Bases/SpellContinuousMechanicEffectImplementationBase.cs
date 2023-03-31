using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;

namespace Spells.Abstract_Types.Implementation_Bases
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