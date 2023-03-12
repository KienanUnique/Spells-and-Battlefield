using UnityEngine;

public abstract class SpellTriggerImplementationBase : ISpellTrigger
{
    public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}