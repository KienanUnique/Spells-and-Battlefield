using Common.Mechanic_Effects.Scriptable_Objects;
using Interfaces;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types
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

            public DamageInstantMechanicImplementation(int damage) => _damage = damage;

            public override void ApplyEffectToTarget(IInteractable target)
            {
                if (target.TryGetComponent(out IDamageable damageableTarget))
                {
                    damageableTarget.HandleDamage(_damage);
                }
            }
        }
    }
}