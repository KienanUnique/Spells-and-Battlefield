using UnityEngine;

public abstract class SpellMovementScriptableObject : ScriptableObject
{
    public abstract void Move(Rigidbody spellRigidbody);
}