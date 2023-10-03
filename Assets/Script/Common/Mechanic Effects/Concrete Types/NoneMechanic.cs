using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Scriptable_Objects;
using Common.Mechanic_Effects.Source;
using Spells;
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

            public void ApplyEffectToTargets(IEnumerable<IInteractable> targets,
                IEffectSourceInformation sourceInformation)
            {
            }

            public void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation)
            {
            }
        }
    }
}