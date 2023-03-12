using UnityEngine;
public interface ISpellTrigger
{
    public SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}