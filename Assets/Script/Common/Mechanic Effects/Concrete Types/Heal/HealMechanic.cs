using Common.Interfaces;
using Common.Mechanic_Effects.Scriptable_Objects;
using Common.Mechanic_Effects.Source;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types.Heal
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

            public override void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation)
            {
                if (target.TryGetComponent(out IHealable healableTarget))
                {
                    healableTarget.HandleHeal(_healPoints, sourceInformation);
                }
            }
        }
    }
}