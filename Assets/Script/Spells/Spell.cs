using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells and Battlefield/Spell System/Spell", order = 0)]
public class Spell : ScriptableObject
{
    public AnimatorOverrideController HandsAnimatorController => _animatorOverrideController;
    [SerializeField] private SpellMechanicEffectScriptableObject _mechanicEffect;
    [SerializeField] private SpellMovementScriptableObject _movement;
    [SerializeField] private SpellObjectController _spellObjectPrefab;
    [SerializeField] private AnimatorOverrideController _animatorOverrideController;
    [SerializeField] private SpellTargetSelecterScriptableObject _targetSelecter;
    [SerializeField] private SpellTriggerScriptableObject _trigger;
    [SerializeField] private List<Spell> _nextSpellsOnFinish;

    private ISpellMechanicEffect SpellMechanicEffect => _mechanicEffect.GetImplementationObject();
    private ISpellMovement SpellObjectMovement => _movement.GetImplementationObject();
    private ISpellTargetSelecter TargetSelecter => _targetSelecter.GetImplementationObject();
    private ISpellTrigger SpellTrigger => _trigger.GetImplementationObject();

    public void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform, ICharacter casterCharacter)
    {
        var spellObjectController = Instantiate(_spellObjectPrefab.gameObject, spawnSpellPosition, spawnSpellRotation).GetComponent<SpellObjectController>();
        spellObjectController.Initialize(SpellMechanicEffect, SpellObjectMovement, TargetSelecter, _nextSpellsOnFinish, SpellTrigger, casterTransform, casterCharacter);
    }
}