using System.Collections.Generic;
using UnityEngine;

public abstract class SpellMechanicEffectScriptableObject : ScriptableObject
{
    protected abstract void ApplyEffectToTarget(ICharacter target);
    public virtual void ApplyEffectToTargets(List<ICharacter> targets)
    {
        targets.ForEach(target => ApplyEffectToTarget(target));
    }
}