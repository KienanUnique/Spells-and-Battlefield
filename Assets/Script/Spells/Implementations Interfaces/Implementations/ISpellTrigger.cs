using UnityEngine;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellTrigger : ISpellImplementation
    {
        public SpellTriggerCheckStatusEnum CheckContact(Collider other);
        public SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
    }
}