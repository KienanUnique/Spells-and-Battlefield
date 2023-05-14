using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "Sphere Selector",
        menuName = "Spells and Battlefield/Spell System/Target Selector/Sphere Enemy Selector", order = 0)]
    public class SphereSelector : SpellTargetSelectorScriptableObject
    {
        [SerializeField] private float _sphereRadius;
        [SerializeField] private bool _ignoreCaster;

        public override ISpellTargetSelector GetImplementationObject() =>
            new SphereEnemySelectorImplementation(_sphereRadius, _ignoreCaster);

        private class SphereEnemySelectorImplementation : SpellTargetSelectorImplementationBase
        {
            private const int LayerMask = Physics.AllLayers;
            private readonly float _sphereRadius;
            private readonly bool _ignoreCasterCollisions;

            public SphereEnemySelectorImplementation(float sphereRadius, bool ignoreCasterCollisions)
            {
                _sphereRadius = sphereRadius;
                _ignoreCasterCollisions = ignoreCasterCollisions;
            }

            public override List<ISpellInteractable> SelectTargets()
            {
                var selectedTargets = new List<ISpellInteractable>();
                var collidersInsideSphere = Physics.OverlapSphere(_spellRigidbody.position, _sphereRadius, LayerMask,
                    QueryTriggerInteraction.Ignore);
                var casterId = -1;
                var casterHaveId = false;
                if (Caster is ISpellInteractable casterSpellInteractable)
                {
                    casterId = casterSpellInteractable.Id;
                    casterHaveId = true;
                }

                foreach (var hitCollider in collidersInsideSphere)
                {
                    if (hitCollider.gameObject.TryGetComponent<ISpellInteractable>(out var target) &&
                        !(_ignoreCasterCollisions && casterHaveId && target.Id == casterId)
                        && !selectedTargets.Exists(handledTarget => handledTarget.Id == target.Id))
                    {
                        selectedTargets.Add(target);
                    }
                }

                return selectedTargets;
            }
        }
    }
}