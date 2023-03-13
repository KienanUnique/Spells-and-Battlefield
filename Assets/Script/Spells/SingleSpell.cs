using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single Spell", menuName = "Spells and Battlefield/Spell System/Single Spell", order = 0)]
public class SingleSpell : ScriptableObject, ISpell
{
    public AnimatorOverrideController CastAnimationAnimatorOverrideController => _castAnimationAnimatorOverrideController;
    [SerializeField] private List<SpellMechanicEffectScriptableObject> _mechanicEffects;
    [SerializeField] private SpellMovementScriptableObject _movement;
    [SerializeField] private SpellObjectController _spellObjectPrefab;
    [SerializeField] private AnimatorOverrideController _castAnimationAnimatorOverrideController;
    [SerializeField] private SpellTargetSelecterScriptableObject _targetSelecter;
    [SerializeField] private SpellTriggerScriptableObject _trigger;
    [SerializeField] private List<SpellBase> _nextSpellsOnFinish;

    private List<ISpellMechanicEffect> SpellMechanicEffects
    {
        get
        {
            var iSpellMechanicsList = new List<ISpellMechanicEffect>();
            _mechanicEffects.ForEach(mechanicEffect => iSpellMechanicsList.Add(mechanicEffect.GetImplementationObject()));
            return iSpellMechanicsList;
        }
    }

    private ISpellMovement SpellObjectMovement => _movement.GetImplementationObject();
    private ISpellTargetSelecter TargetSelecter => _targetSelecter.GetImplementationObject();
    private ISpellTrigger SpellTrigger => _trigger.GetImplementationObject();

    public void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform, ISpellInteractable casterCharacter)
    {
        var spellObjectController = Instantiate(_spellObjectPrefab.gameObject, spawnSpellPosition, spawnSpellRotation).GetComponent<SpellObjectController>();
        spellObjectController.Initialize(SpellMechanicEffects, SpellObjectMovement, TargetSelecter, _nextSpellsOnFinish, SpellTrigger, casterTransform, casterCharacter);
    }
}