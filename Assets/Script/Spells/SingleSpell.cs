using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single Spell", menuName = "Spells and Battlefield/Spell System/Single Spell", order = 0)]
public class SingleSpell : ScriptableObject, ISpell
{
    public AnimatorOverrideController CastAnimationAnimatorOverrideController => _castAnimationAnimatorOverrideController;
    [SerializeField] private SpellMovementScriptableObject _movement;
    [SerializeField] private List<SpellApplierScriptableObject> _appliers;
    [SerializeField] private SpellTriggerScriptableObject _mainTrigger;
    [SerializeField] private SpellObjectController _spellObjectPrefab;
    [SerializeField] private AnimatorOverrideController _castAnimationAnimatorOverrideController;
    [SerializeField] private List<SpellBase> _nextSpellsOnFinish;

    private List<ISpellApplier> SpellAppliers
    {
        get
        {
            var iSpellAppliersList = new List<ISpellApplier>();
            _appliers.ForEach(spellApplier => iSpellAppliersList.Add(spellApplier.GetImplementationObject()));
            return iSpellAppliersList;
        }
    }

    private ISpellMovement SpellObjectMovement => _movement.GetImplementationObject();
    private ISpellTriggerable SpellTrigger => _mainTrigger.GetImplementationObject();

    public void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform, ISpellInteractable casterCharacter)
    {
        var spellObjectController = Instantiate(_spellObjectPrefab.gameObject, spawnSpellPosition, spawnSpellRotation).GetComponent<SpellObjectController>();
        spellObjectController.Initialize(SpellObjectMovement, _nextSpellsOnFinish, SpellAppliers, SpellTrigger, casterTransform, casterCharacter);
    }
}