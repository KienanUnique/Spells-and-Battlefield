using System.Collections.Generic;
using Common.Mechanic_Effects.Scriptable_Objects;
using Interfaces;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types
{
    [CreateAssetMenu(fileName = "None Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "None Mechanic", order = 0)]
    public class NoneMechanic : MechanicEffectScriptableObject
    {
        public override IMechanicEffect GetImplementationObject()
        {
            return new NoneMechanicImplementation();
        }

        private class NoneMechanicImplementation : IMechanicEffect
        {
            public void Initialize(Rigidbody spellRigidbody, ICaster caster)
            {
            }

            public void ApplyEffectToTargets(IReadOnlyCollection<IInteractable> targets)
            {
            }

            public void ApplyEffectToTarget(IInteractable target)
            {
            }
        }
    }
}