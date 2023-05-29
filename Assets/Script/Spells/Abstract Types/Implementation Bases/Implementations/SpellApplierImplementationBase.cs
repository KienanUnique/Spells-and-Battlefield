using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellApplierImplementationBase : SpellImplementationBase, ISpellApplier
    {
        public abstract SpellTriggerCheckStatusEnum CheckContact(Collider other);
        public abstract SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize);
        public abstract void HandleRollbackableEffects();
    }
}