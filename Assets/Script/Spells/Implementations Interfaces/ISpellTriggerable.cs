using UnityEngine;
public interface ISpellTriggerable : ISpellImplementation
{
    public SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}