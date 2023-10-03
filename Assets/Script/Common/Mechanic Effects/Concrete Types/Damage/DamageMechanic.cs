using Common.Interfaces;
using Common.Mechanic_Effects.Scriptable_Objects;
using Common.Mechanic_Effects.Source;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types.Damage
{
    [CreateAssetMenu(fileName = "Damage Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Damage Mechanic", order = 0)]
    public class DamageMechanic : MechanicEffectScriptableObject
    {
        [SerializeField] private int _damage;

        public override IMechanicEffect GetImplementationObject()
        {
            return new DamageInstantMechanicImplementation(_damage);
        }

        private class DamageInstantMechanicImplementation : InstantMechanicEffectImplementationBase
        {
            private readonly int _damage;

            public DamageInstantMechanicImplementation(int damage)
            {
                _damage = damage;
            }

            public override void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation)
            {
                if (target.TryGetComponent(out IDamageable damageableTarget))
                {
                    damageableTarget.HandleDamage(_damage, sourceInformation);
                }
            }
        }
    }
}