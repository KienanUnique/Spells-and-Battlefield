using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere Enemy Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/Sphere Enemy Selector", order = 0)]
public class SphereEnemySelector : TargetSelecterScriptableObject
{
    [SerializeField] private float _sphereRadius;
    public override List<ICharacter> SelectTargets(Vector3 spellPosition, ICharacter casterCharacter)
    {
        var selectedTargets = new List<ICharacter>();
        Collider[] collidersInsideSphere = Physics.OverlapSphere(spellPosition, _sphereRadius);
        foreach (var hitCollider in collidersInsideSphere)
        {
            if (hitCollider.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                selectedTargets.Add(enemyController);
            }
        }
        return selectedTargets;
    }
}