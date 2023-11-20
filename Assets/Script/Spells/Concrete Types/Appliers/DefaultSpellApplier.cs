using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Scriptable_Objects;
using Common.Mechanic_Effects.Source;
using Common.Readonly_Transform;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Data_For_Spell_Implementation;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Appliers
{
    [CreateAssetMenu(fileName = "Default Spell Applier",
        menuName = ScriptableObjectsMenuDirectories.SpellAppliersDirectory + "Default Spell Applier", order = 0)]
    public class DefaultSpellApplier : SpellApplierScriptableObject
    {
        [SerializeField] private List<MechanicEffectScriptableObject> _spellMechanicEffects;
        [SerializeField] private SpellTargetSelectorScriptableObject _targetSelector;
        [SerializeField] private SpellTriggerScriptableObject _spellTrigger;

        public override ISpellApplier GetImplementationObject()
        {
            var iSpellMechanicsList = new List<IMechanicEffect>();
            _spellMechanicEffects.ForEach(spellMechanicEffect =>
                iSpellMechanicsList.Add(spellMechanicEffect.GetImplementationObject()));
            return new DefaultSpellApplierImplementation(iSpellMechanicsList, _targetSelector.GetImplementationObject(),
                _spellTrigger.GetImplementationObject());
        }

        private class DefaultSpellApplierImplementation : SpellApplierImplementationBase
        {
            private readonly List<IMechanicEffect> _spellMechanicEffects;
            private readonly ISpellTrigger _spellTrigger;
            private readonly ISpellTargetSelector _targetSelector;
            private IEffectSourceInformation _effectSourceInformation;

            public DefaultSpellApplierImplementation(List<IMechanicEffect> spellMechanicEffects,
                ISpellTargetSelector targetSelector, ISpellTrigger spellTrigger)
            {
                _spellMechanicEffects = spellMechanicEffects;
                _targetSelector = targetSelector;
                _spellTrigger = spellTrigger;
            }

            public override void Initialize(IDataForSpellImplementation data)
            {
                var spellImplementations = new List<ISpellImplementation> {_targetSelector, _spellTrigger};

                spellImplementations.ForEach(spellImplementation => spellImplementation.Initialize(data));

                _effectSourceInformation = new EffectSourceInformation(EffectSourceType.External,
                    new ReadonlyTransform(data.SpellRigidbody.transform));
            }

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other)
            {
                SpellTriggerCheckStatusEnum response = _spellTrigger.CheckContact(other);
                HandleSpellTriggerResponse(response);
                return response;
            }

            public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
            {
                SpellTriggerCheckStatusEnum response = _spellTrigger.CheckTime(timePassedFromInitialize);
                HandleSpellTriggerResponse(response);
                return response;
            }

            public override void HandleRollbackableEffects()
            {
                foreach (IMechanicEffect effect in _spellMechanicEffects)
                {
                    if (effect is IMechanicEffectWithRollback effectWithRollback)
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
                IReadOnlyList<ISpellInteractable> selectedTargets = _targetSelector.SelectTargets();
                _spellMechanicEffects.ForEach(mechanicEffect =>
                    mechanicEffect.ApplyEffectToTargets(new List<IInteractable>(selectedTargets),
                        _effectSourceInformation));
            }
        }
    }
}