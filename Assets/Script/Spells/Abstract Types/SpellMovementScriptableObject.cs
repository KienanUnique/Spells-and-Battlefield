using UnityEngine;

public abstract class SpellMovementScriptableObject : ScriptableObject
{
#nullable enable
    public abstract void Move(Rigidbody spellRigidbody, Transform? fromCastObjectTransform, float timePassedFromInitialize);
#nullable disable
}