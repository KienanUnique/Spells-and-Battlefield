﻿using Common.Mechanic_Effects.Scriptable_Objects;
using Interfaces;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types
{
    [CreateAssetMenu(fileName = "Push Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Push Mechanic", order = 0)]
    public class PushMechanic : MechanicEffectScriptableObject
    {
        [SerializeField] private float _forceValue;

        public override IMechanicEffect GetImplementationObject()
        {
            return new PushMechanicImplementation(_forceValue);
        }

        private class PushMechanicImplementation : InstantMechanicEffectImplementationBase
        {
            private readonly float _forceValue;

            public PushMechanicImplementation(float forceValue) => _forceValue = forceValue;

            public override void ApplyEffectToTarget(IInteractable target)
            {
                if (target.TryGetComponent(out IPhysicsInteractable physicsTarget))
                {
                    var force = (physicsTarget.CurrentPosition - _spellRigidbody.position).normalized;
                    force *= _forceValue;
                    physicsTarget.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }
}