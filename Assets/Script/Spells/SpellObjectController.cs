using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpellObjectController : MonoBehaviour
{
    private float TimePassedFromInitialize => Time.time - _initializeTime;
    private ISpellMechanicEffect _spellMechanicEffect;
    private Rigidbody _rigidbody;
    private ISpellMovement _spellMovement;
    private ISpellTargetSelecter _targetSelecter;
    private ISpellTrigger _spellTrigger;
    private ICharacter _casterCharacter;
    private List<SingleSpell> _nextSpellsOnFinish;
    private float _initializeTime;
    private bool _wasInitialised = false;
#nullable enable
    private Transform? _castObjectTransform;
#nullable disable

#nullable enable
    public void Initialize(ISpellMechanicEffect spellMechanicEffect, ISpellMovement spellMovement,
    ISpellTargetSelecter targetSelecter, List<SingleSpell> nextSpellsOnFinish, ISpellTrigger spellTrigger,
    Transform? castObjectTransform, ICharacter casterCharacter)
    {
        _spellMechanicEffect = spellMechanicEffect;
        _spellMovement = spellMovement;
        _targetSelecter = targetSelecter;
        _spellTrigger = spellTrigger;
        _castObjectTransform = castObjectTransform;
        List<ISpellImplementation> _spellImplementations = new List<ISpellImplementation>()
        {
            _spellMechanicEffect,
            _spellMovement,
            _targetSelecter,
            _spellTrigger
        };

        _spellImplementations.ForEach(_spellImplementation => _spellImplementation.Initialize(_rigidbody, castObjectTransform, casterCharacter));

        _casterCharacter = casterCharacter;
        _nextSpellsOnFinish = nextSpellsOnFinish;

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
        var selectedTargets = _targetSelecter.SelectTargets();
        _spellMechanicEffect.ApplyEffectToTargets(selectedTargets);
    }

    private void HandleFinishSpell()
    {
        _nextSpellsOnFinish.ForEach(spell => spell.Cast(transform.position, transform.rotation, transform, _casterCharacter));
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
            _spellMovement.UpdatePosition();
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