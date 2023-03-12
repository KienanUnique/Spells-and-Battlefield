using UnityEngine;

public abstract class SpellTriggerScriptableObject : ScriptableObject
{
    public abstract ISpellTrigger GetImplementationObject();
}