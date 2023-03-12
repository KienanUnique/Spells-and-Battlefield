using UnityEngine;

public abstract class SpellMovementScriptableObject : ScriptableObject
{
    public abstract ISpellMovement GetImplementationObject();
}