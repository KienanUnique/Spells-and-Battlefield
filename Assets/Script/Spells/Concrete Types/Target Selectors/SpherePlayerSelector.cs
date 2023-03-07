using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere Player Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/Sphere Player Selector", order = 0)]
public class SpherePlayerSelector : TargetSelecterScriptableObject
{
    [SerializeField] private float _sphereRadius;
    public override List<ICharacter> SelectTargets(Vector3 spellPosition)
    {
        var selectedTargets = new List<ICharacter>();
        Collider[] collidersInsideSphere = Physics.OverlapSphere(spellPosition, _sphereRadius);
        foreach (var hitCollider in collidersInsideSphere)
        {
            if (hitCollider.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                selectedTargets.Add(playerController);
                break;
            }
        }
        return selectedTargets;
    }
}