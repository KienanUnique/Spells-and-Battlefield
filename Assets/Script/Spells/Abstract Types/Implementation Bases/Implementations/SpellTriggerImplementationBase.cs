using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellTriggerImplementationBase : SpellImplementationBase, ISpellTrigger
    {
        public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
        public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
    }
}