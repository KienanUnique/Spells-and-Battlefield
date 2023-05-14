using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Appliers
{
    [CreateAssetMenu(fileName = "Default Spell Applier",
        menuName = "Spells and Battlefield/Spell System/Spell Appliers/Default Spell Applier", order = 0)]
    public class DefaultSpellApplier : SpellApplierScriptableObject
    {
        [SerializeField] private List<SpellMechanicEffectScriptableObject> _spellMechanicEffects;
        [SerializeField] private SpellTargetSelectorScriptableObject _targetSelector;
        [SerializeField] private SpellTriggerScriptableObject _spellTrigger;

        public override ISpellApplier GetImplementationObject()
        {
            var iSpellMechanicsList = new List<ISpellMechanicEffect>();
            _spellMechanicEffects.ForEach(spellMechanicEffect =>
                iSpellMechanicsList.Add(spellMechanicEffect.GetImplementationObject()));
            return new DefaultSpellApplierImplementation(iSpellMechanicsList, _targetSelector.GetImplementationObject(),
                _spellTrigger.GetImplementationObject());
        }

        private class DefaultSpellApplierImplementation : SpellApplierImplementationBase
        {
            private readonly List<ISpellMechanicEffect> _spellMechanicEffects;
            private readonly ISpellTargetSelector _targetSelector;
            private readonly ISpellTrigger _spellTrigger;

            public DefaultSpellApplierImplementation(List<ISpellMechanicEffect> spellMechanicEffects,
                ISpellTargetSelector targetSelector, ISpellTrigger spellTrigger)
            {
                _spellMechanicEffects = spellMechanicEffects;
                _targetSelector = targetSelector;
                _spellTrigger = spellTrigger;
            }

            public override void Initialize(Rigidbody spellRigidbody, ICaster caster)
            {
                var spellImplementations = new List<ISpellImplementation>()
                {
                    _targetSelector,
                    _spellTrigger
                };
                spellImplementations.AddRange(_spellMechanicEffects);

                spellImplementations.ForEach(spellImplementation =>
                    spellImplementation.Initialize(spellRigidbody, caster));
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

            public override void HandleRollbackableEffects()
            {
                foreach (var effect in _spellMechanicEffects)
                {
                    if (effect is ISpellMechanicEffectWithRollback effectWithRollback)
                    {
                        effectWithRollback.Rollback();
                    }
                }
            }

            private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
            {
                if (response == SpellTriggerCheckStatusEnum.HandleEffect ||
                    response == SpellTriggerCheckStatusEnum.Finish)
                {
                    HandleSpellEffect();
                }
            }

            private void HandleSpellEffect()
            {
                var selectedTargets = _targetSelector.SelectTargets();
                _spellMechanicEffects.ForEach(mechanicEffect => mechanicEffect.ApplyEffectToTargets(selectedTargets));
            }
        }
    }
}