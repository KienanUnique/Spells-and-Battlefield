using UnityEngine;

public abstract class SpellTargetSelecterScriptableObject : ScriptableObject
{
    public abstract ISpellTargetSelecter GetImplementationObject();
}
