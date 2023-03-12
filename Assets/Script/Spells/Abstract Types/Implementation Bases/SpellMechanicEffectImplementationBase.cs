using System.Collections.Generic;

public abstract class SpellMechanicEffectImplementationBase : ISpellMechanicEffect
{
    protected abstract void ApplyEffectToTarget(ICharacter target);
    public virtual void ApplyEffectToTargets(List<ICharacter> targets)
    {
        targets.ForEach(target => ApplyEffectToTarget(target));
    }
}