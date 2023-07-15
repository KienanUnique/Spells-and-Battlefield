using System;
using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction;
using Spells.Implementations_Interfaces.Implementations;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone
{
    public class SpellZoneController : MechanismsTriggerBase, IInitializableSpellZoneController
    {
        private List<ISpellType> _typesToTriggerOn;
        private ITriggerOnSpellInteraction _triggerOnSpellInteraction;

        public void Initialize(List<ISpellType> typesToTriggerOn, ITriggerOnSpellInteraction triggerOnSpellInteraction)
        {
            _typesToTriggerOn = typesToTriggerOn;
            _triggerOnSpellInteraction = triggerOnSpellInteraction;
            SetInitializedStatus();
        }

        public override event Action Triggered;

        protected override void SubscribeOnEvents()
        {
            _triggerOnSpellInteraction.SpellTypeInteractionDetected += OnSpellTypeInteractionDetected;
        }

        protected override void UnsubscribeFromEvents()
        {
            _triggerOnSpellInteraction.SpellTypeInteractionDetected -= OnSpellTypeInteractionDetected;
        }

        private void OnSpellTypeInteractionDetected(ISpellType spellType)
        {
            if (_typesToTriggerOn.Contains(spellType))
            {
                Triggered?.Invoke();
            }
        }
    }
}