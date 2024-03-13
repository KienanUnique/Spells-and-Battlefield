using System.Collections.Generic;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "Sphere Selector",
        menuName = ScriptableObjectsMenuDirectories.SpellTargetSelectorDirectory + "Sphere Enemy Selector", order = 0)]
    public class SphereSelector : SpellTargetSelectorScriptableObject
    {
        [SerializeField] private float _sphereRadius = 0.7f;
        [SerializeField] private bool _ignoreCaster;

        public override ISpellTargetSelector GetImplementationObject()
        {
            return new SphereEnemySelectorImplementation(_sphereRadius, _ignoreCaster);
        }

        private class SphereEnemySelectorImplementation : SpellTargetSelectorImplementationBase
        {
            private const int LayerMask = Physics.AllLayers;
            private readonly bool _ignoreCasterCollisions;
            private readonly float _sphereRadius;

            public SphereEnemySelectorImplementation(float sphereRadius, bool ignoreCasterCollisions)
            {
                _sphereRadius = sphereRadius;
                _ignoreCasterCollisions = ignoreCasterCollisions;
            }

            public override IReadOnlyList<ISpellInteractable> SelectTargets()
            {
                var selectedTargets = new List<ISpellInteractable>();
                Collider[] collidersInsideSphere = Physics.OverlapSphere(_spellTransform.position, _sphereRadius,
                    LayerMask, QueryTriggerInteraction.Ignore);
                int casterId = -1;
                var casterHaveId = false;
                if (Caster is ISpellInteractable casterSpellInteractable)
                {
                    casterId = casterSpellInteractable.Id;
                    casterHaveId = true;
                }

                foreach (Collider hitCollider in collidersInsideSphere)
                {
                    if (hitCollider.gameObject.TryGetComponent(out ISpellInteractable target) &&
                        !(_ignoreCasterCollisions && casterHaveId && target.Id == casterId) &&
                        !selectedTargets.Exists(handledTarget => handledTarget.Id == target.Id))
                    {
                        selectedTargets.Add(target);
                    }
                }

                return selectedTargets;
            }
        }
    }
}