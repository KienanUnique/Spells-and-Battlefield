using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpellObjectController : MonoBehaviour
{
    private float TimePassedFromInitialize => Time.time - _initializeTime;
    private SpellMechanicEffectScriptableObject _spellMechanicEffect;
    private Rigidbody _rigidbody;
    private SpellMovementScriptableObject _spellMovement;
    private TargetSelecterScriptableObject _targetSelecter;
    private SpellTriggerScriptableObject _spellTrigger;
    private ICharacter _casterCharacter;
    private List<Spell> _nextSpellsOnFinish;
    private float _initializeTime;
    private bool _wasInitialised = false;
#nullable enable
    private Transform? _casterTransform;
    private Transform? _thisTransform;
#nullable disable

#nullable enable
    public void Initialize(SpellMechanicEffectScriptableObject spellMechanicEffect, SpellMovementScriptableObject spellMovement,
    TargetSelecterScriptableObject targetSelecter, List<Spell> nextSpellsOnFinish, SpellTriggerScriptableObject spellTrigger,
    Transform? casterTransform, ICharacter casterCharacter)
    {
        _spellMechanicEffect = spellMechanicEffect;
        _spellMovement = spellMovement;
        _targetSelecter = targetSelecter;
        _nextSpellsOnFinish = nextSpellsOnFinish;
        _spellTrigger = spellTrigger;
        _casterTransform = casterTransform;
        _casterCharacter = casterCharacter;

        _thisTransform = transform;

        _initializeTime = Time.time;

        _wasInitialised = true;
    }
#nullable disable

    private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
    {
        switch (response)
        {
            case SpellTriggerCheckStatusEnum.HandleEffect:
                HandleSpellEffect();
                break;
            case SpellTriggerCheckStatusEnum.Finish:
                HandleSpellEffect();
                HandleFinishSpell();
                break;
        }
    }

    private void HandleSpellEffect()
    {
        var selectedTargets = _targetSelecter.SelectTargets(_thisTransform.position, _casterCharacter);
        _spellMechanicEffect.ApplyEffectToTargets(selectedTargets);
    }

    private void HandleFinishSpell()
    {
        _nextSpellsOnFinish.ForEach(spell => spell.Cast(_thisTransform.position, _thisTransform.rotation, _thisTransform, _casterCharacter));
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_wasInitialised)
        {
            _spellMovement.Move(_rigidbody, _casterTransform, TimePassedFromInitialize);
            HandleSpellTriggerResponse(_spellTrigger.CheckTime(TimePassedFromInitialize));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_wasInitialised)
        {
            HandleSpellTriggerResponse(_spellTrigger.CheckContact(other));
        }
    }

}