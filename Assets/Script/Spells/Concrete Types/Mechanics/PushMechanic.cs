using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Push Mechanic",
        menuName = "Spells and Battlefield/Spell System/Mechanic/Push Mechanic", order = 0)]
    public class PushMechanic : SpellMechanicEffectScriptableObject
    {
        [SerializeField] private float _forceValue;

        public override ISpellMechanicEffect GetImplementationObject()
        {
            return new PushMechanicImplementation(_forceValue);
        }

        private class PushMechanicImplementation : SpellInstantMechanicEffectImplementationBase
        {
            private readonly float _forceValue;

            public PushMechanicImplementation(float forceValue) => _forceValue = forceValue;

            public override void ApplyEffectToTarget(ISpellInteractable target)
            {
                if (!(target is IPhysicsInteractable physicsTarget)) return;
                var force = (physicsTarget.CurrentPosition - _spellRigidbody.position).normalized;
                force *= _forceValue;
                physicsTarget.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}