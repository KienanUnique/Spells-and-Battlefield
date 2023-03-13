using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpellObjectController : MonoBehaviour
{
    private float TimePassedFromInitialize => Time.time - _initializeTime;
    private List<ISpellMechanicEffect> _spellMechanicEffects;
    private Rigidbody _rigidbody;
    private ISpellMovement _spellMovement;
    private ISpellTargetSelecter _targetSelecter;
    private ISpellTrigger _spellTrigger;
    private ISpellInteractable _casterCharacter;
    private List<SpellBase> _nextSpellsOnFinish;
    private float _initializeTime;
    private bool _wasInitialised = false;
#nullable enable
    private Transform? _casterTransform;
#nullable disable

#nullable enable
    public void Initialize(List<ISpellMechanicEffect> spellMechanicEffects, ISpellMovement spellMovement,
    ISpellTargetSelecter targetSelecter, List<SpellBase> nextSpellsOnFinish, ISpellTrigger spellTrigger,
    Transform? casterTransform, ISpellInteractable casterCharacter)
    {
        _spellMechanicEffects = spellMechanicEffects;
        _spellMovement = spellMovement;
        _targetSelecter = targetSelecter;
        _spellTrigger = spellTrigger;
        _casterTransform = casterTransform;
        List<ISpellImplementation> _spellImplementations = new List<ISpellImplementation>()
        {
            _spellMovement,
            _targetSelecter,
            _spellTrigger
        };
        _spellImplementations.AddRange(_spellMechanicEffects);

        _spellImplementations.ForEach(_spellImplementation => _spellImplementation.Initialize(_rigidbody, casterTransform, casterCharacter));

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
        _spellMechanicEffects.ForEach(mechanicEffect => mechanicEffect.ApplyEffectToTargets(selectedTargets));
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