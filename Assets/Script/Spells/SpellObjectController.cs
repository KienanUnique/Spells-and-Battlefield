using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpellObjectController : MonoBehaviour
{
    private SpellMechanicEffectScriptableObject _spellMechanicEffect;
    private Rigidbody _rigidbody;
    private SpellMovementScriptableObject _spellMovement;
    private TargetSelecterScriptableObject _targetSelecter;
    private SpellTriggerScriptableObject _spellTrigger;
    private List<Spell> _nextSpellsOnFinish;
    private float _initializeTime;
    private bool _wasInitialised = false;
    private float TimePassedFromInitialize => Time.time - _initializeTime;
    public void Initialize(SpellMechanicEffectScriptableObject spellMechanicEffect, SpellMovementScriptableObject spellMovement,
    TargetSelecterScriptableObject targetSelecter, List<Spell> nextSpellsOnFinish, SpellTriggerScriptableObject spellTrigger)
    {
        _spellMechanicEffect = spellMechanicEffect;
        _spellMovement = spellMovement;
        _targetSelecter = targetSelecter;
        _nextSpellsOnFinish = nextSpellsOnFinish;
        _spellTrigger = spellTrigger;

        _initializeTime = Time.time;

        _wasInitialised = true;
    }

    private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
    {
        switch (response)
        {
            case SpellTriggerCheckStatusEnum.HandleEffect:
                HandleSpellEffect();
                break;
            case SpellTriggerCheckStatusEnum.Finish:
                HandleFinishSpell();
                break;
        }
    }

    private void HandleSpellEffect()
    {
        var selectedTargets = _targetSelecter.SelectTargets(transform.position);
        _spellMechanicEffect.ApplyEffectToTargets(selectedTargets);
    }

    private void HandleFinishSpell()
    {
        _nextSpellsOnFinish.ForEach(spell => spell.Cast(transform.position, transform.rotation));
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
            _spellMovement.Move(_rigidbody);
            HandleSpellTriggerResponse(_spellTrigger.CheckTime(TimePassedFromInitialize));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_wasInitialised)
        {
            HandleSpellTriggerResponse(_spellTrigger.CheckCollisionEnter(other));
        }
    }

}