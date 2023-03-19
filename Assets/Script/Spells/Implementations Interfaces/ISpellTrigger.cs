using UnityEngine;

namespace Spells.Implementations_Interfaces
{
    public interface ISpellTrigger : ISpellImplementation
    {
        public SpellTriggerCheckStatusEnum CheckContact(Collider other);
        public SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
    }
}