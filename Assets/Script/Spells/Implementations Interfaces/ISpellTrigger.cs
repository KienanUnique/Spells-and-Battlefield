using UnityEngine;
public interface ISpellTrigger : ISpellImplementation
{
    public SpellTriggerCheckStatusEnum CheckContact(Collider other);
    public SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
}