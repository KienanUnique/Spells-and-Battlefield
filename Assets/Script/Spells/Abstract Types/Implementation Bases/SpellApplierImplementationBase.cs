using UnityEngine;

public abstract class SpellApplierImplementationBase : SpellImplementationBase, ISpellApplier
{
    public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}