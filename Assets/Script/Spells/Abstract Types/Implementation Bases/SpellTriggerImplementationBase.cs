using UnityEngine;

public abstract class SpellTriggerImplementationBase : SpellImplementationBase, ISpellTrigger
{
    public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}