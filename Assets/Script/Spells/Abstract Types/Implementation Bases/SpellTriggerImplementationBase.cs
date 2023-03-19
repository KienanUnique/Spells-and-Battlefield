using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellTriggerImplementationBase : SpellImplementationBase, ISpellTrigger
    {
        public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
        public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
    }
}