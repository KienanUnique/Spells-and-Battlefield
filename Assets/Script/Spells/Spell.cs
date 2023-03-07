using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells and Battlefield/Spell System/Spell", order = 0)]
public class Spell : ScriptableObject
{
    public AnimatorOverrideController HandsAnimatorController => _useSpellHandsAnimatorController;
    [SerializeField] private SpellMechanicEffectScriptableObject _spellMechanicEffect;
    [SerializeField] private SpellMovementScriptableObject _spellObjectMovementController;
    [SerializeField] private SpellObjectController _spellObjectPrefab;
    [SerializeField] private AnimatorOverrideController _useSpellHandsAnimatorController;
    [SerializeField] private TargetSelecterScriptableObject _targetSelecter;
    [SerializeField] private SpellTriggerScriptableObject _spellTrigger;
    [SerializeField] private List<Spell> _nextSpellsOnFinish;

    public void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform, ICharacter casterCharacter)
    {
        var spellObjectController = Instantiate(_spellObjectPrefab.gameObject, spawnSpellPosition, spawnSpellRotation).GetComponent<SpellObjectController>();
        spellObjectController.Initialize(_spellMechanicEffect, _spellObjectMovementController, _targetSelecter, _nextSpellsOnFinish, _spellTrigger, casterTransform, casterCharacter);
    }
}