using UnityEngine;

public abstract class SpellMechanicEffectScriptableObject : ScriptableObject
{
    public abstract ISpellMechanicEffect GetImplementationObject();
}