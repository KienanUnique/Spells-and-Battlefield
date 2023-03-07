using UnityEngine;

public abstract class SpellTriggerScriptableObject : ScriptableObject
{
    public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}
