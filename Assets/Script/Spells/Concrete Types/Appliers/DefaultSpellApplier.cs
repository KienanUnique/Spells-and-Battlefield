using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Spell Applier", menuName = "Spells and Battlefield/Spell System/Spell Appliers/Default Spell Applier", order = 0)]
public class DefaultSpellApplier : SpellApplierScriptableObject
{
    [SerializeField] private List<SpellMechanicEffectScriptableObject> _spellMechanicEffects;
    [SerializeField] private SpellTargetSelecterScriptableObject _targetSelecter;
    [SerializeField] private SpellTriggerScriptableObject _spellTrigger;

    public override ISpellApplier GetImplementationObject()
    {
        var iSpellMechanicsList = new List<ISpellMechanicEffect>();
        _spellMechanicEffects.ForEach(spellMechanicEffect => iSpellMechanicsList.Add(spellMechanicEffect.GetImplementationObject()));
        return new DefaultSpellApplierImplementation(iSpellMechanicsList, _targetSelecter.GetImplementationObject(), _spellTrigger.GetImplementationObject());
    }

    private class DefaultSpellApplierImplementation : SpellApplierImplementationBase
    {
        private List<ISpellMechanicEffect> _spellMechanicEffects;
        private ISpellTargetSelecter _targetSelecter;
        private ISpellTriggerable _spellTrigger;

        public DefaultSpellApplierImplementation(List<ISpellMechanicEffect> spellMechanicEffects, ISpellTargetSelecter targetSelecter, ISpellTriggerable spellTrigger)
        {
            _spellMechanicEffects = spellMechanicEffects;
            _targetSelecter = targetSelecter;
            _spellTrigger = spellTrigger;
        }

        public override void Initialize(Rigidbody spellRigidbody, Transform casterTransform, ISpellInteractable casterCharacter)
        {
            List<ISpellImplementation> _spellImplementations = new List<ISpellImplementation>()
        {
            _targetSelecter,
            _spellTrigger
        };
            _spellImplementations.AddRange(_spellMechanicEffects);

            _spellImplementations.ForEach(_spellImplementation => _spellImplementation.Initialize(spellRigidbody, casterTransform, casterCharacter));
        }

        public override SpellTriggerCheckStatusEnum CheckContact(Collider other)
        {
            var response = _spellTrigger.CheckContact(other);
            HandleSpellTriggerResponse(response);
            return response;
        }

        public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
        {
            var response = _spellTrigger.CheckTime(timePassedFromInitialize);
            HandleSpellTriggerResponse(response);
            return response;
        }

        private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
        {
            if (response == SpellTriggerCheckStatusEnum.HandleEffect || response == SpellTriggerCheckStatusEnum.Finish)
            {
                HandleSpellEffect();
            }
        }

        private void HandleSpellEffect()
        {
            var selectedTargets = _targetSelecter.SelectTargets();
            _spellMechanicEffects.ForEach(mechanicEffect => mechanicEffect.ApplyEffectToTargets(selectedTargets));
        }
    }
}