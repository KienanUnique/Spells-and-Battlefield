using Common.Mechanic_Effects.Scriptable_Objects;
using Interfaces;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types
{
    [CreateAssetMenu(fileName = "Heal Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Heal Mechanic", order = 0)]
    public class HealMechanic : MechanicEffectScriptableObject
    {
        [SerializeField] private int _healPoints;

        public override IMechanicEffect GetImplementationObject()
        {
            return new HealInstantMechanicImplementation(_healPoints);
        }

        private class HealInstantMechanicImplementation : InstantMechanicEffectImplementationBase
        {
            private readonly int _healPoints;

            public HealInstantMechanicImplementation(int healPoints)
            {
                _healPoints = healPoints;
            }

            public override void ApplyEffectToTarget(IInteractable target)
            {
                if (target.TryGetComponent(out IHealable healableTarget))
                {
                    healableTarget.HandleHeal(_healPoints);
                }
            }
        }
    }
}