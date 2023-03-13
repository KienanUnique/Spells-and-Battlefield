using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/Sphere Enemy Selector", order = 0)]
public class SphereSelector : SpellTargetSelecterScriptableObject
{
    [SerializeField] private float _sphereRadius;
    [SerializeField] private bool _ignoreCaster;

    public override ISpellTargetSelecter GetImplementationObject() => new SphereEnemySelectorImplementation(_sphereRadius, _ignoreCaster);
    private class SphereEnemySelectorImplementation : SpellTargetSelecterImplementationBase
    {
        private float _sphereRadius;
        private bool _ignoreCasterCollisions;

        public SphereEnemySelectorImplementation(float sphereRadius, bool ignoreCasterCollisions)
        {
            _sphereRadius = sphereRadius;
            _ignoreCasterCollisions = ignoreCasterCollisions;
        }

        public override List<ISpellInteractable> SelectTargets()
        {
            var selectedTargets = new List<ISpellInteractable>();
            Collider[] collidersInsideSphere = Physics.OverlapSphere(_spellRigidbody.position, _sphereRadius);
            foreach (var hitCollider in collidersInsideSphere)
            {
                if (hitCollider.gameObject.TryGetComponent<SpellGameObjectInterface>(out SpellGameObjectInterface spellGameObjectInterface) && !(_ignoreCasterCollisions && spellGameObjectInterface.Id == _casterInterface.Id))
                {
                    selectedTargets.Add(spellGameObjectInterface);
                }
            }
            return selectedTargets;
        }
    }
}