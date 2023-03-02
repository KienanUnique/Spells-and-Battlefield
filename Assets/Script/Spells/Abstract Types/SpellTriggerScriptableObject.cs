using UnityEngine;

public abstract class SpellTriggerScriptableObject : ScriptableObject
{
    public abstract SpellTriggerCheckStatusEnum CheckCollisionEnter(Collision other);
    public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}
