using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpellObjectController : MonoBehaviour
{
    private float TimePassedFromInitialize => Time.time - _initializeTime;
    private Rigidbody _rigidbody;
    private ISpellMovement _spellMovement;
    private ISpellInteractable _casterCharacter;
    private List<SpellBase> _nextSpellsOnFinish;
    private List<ISpellApplier> _spellAppliers;
    private ISpellTriggerable _spellMainTrigger;
    private float _initializeTime;
    private bool _wasInitialised = false;
#nullable enable
    private Transform? _casterTransform;
#nullable disable

#nullable enable
    public void Initialize(ISpellMovement spellMovement, List<SpellBase> nextSpellsOnFinish, List<ISpellApplier> spellAppliers, ISpellTriggerable spellMainTrigger,
    Transform? casterTransform, ISpellInteractable casterCharacter)
    {
        _spellMovement = spellMovement;
        _spellAppliers = spellAppliers;
        _spellMainTrigger = spellMainTrigger;
        _casterTransform = casterTransform;

        List<ISpellImplementation> _spellImplementations = new List<ISpellImplementation>() {
            _spellMovement,
            _spellMainTrigger
        };
        _spellImplementations.AddRange(_spellAppliers);

        _spellImplementations.ForEach(_spellImplementation => _spellImplementation.Initialize(_rigidbody, casterTransform, casterCharacter));

        _casterCharacter = casterCharacter;
        _nextSpellsOnFinish = nextSpellsOnFinish;

        _initializeTime = Time.time;
        _wasInitialised = true;
    }
#nullable disable

    private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
    {
        if (response == SpellTriggerCheckStatusEnum.Finish)
        {
            HandleFinishSpell();
        }
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

            float commonTimePassedFromInitialize = TimePassedFromInitialize;
            _spellAppliers.ForEach(spellApplier => spellApplier.CheckTime(commonTimePassedFromInitialize));
            HandleSpellTriggerResponse(_spellMainTrigger.CheckTime(commonTimePassedFromInitialize));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_wasInitialised)
        {
            _spellAppliers.ForEach(spellApplier => spellApplier.CheckContact(other));
            HandleSpellTriggerResponse(_spellMainTrigger.CheckContact(other));
        }
    }

}