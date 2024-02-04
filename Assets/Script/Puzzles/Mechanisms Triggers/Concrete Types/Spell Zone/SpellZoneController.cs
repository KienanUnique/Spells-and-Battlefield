using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction;
using Spells.Implementations_Interfaces.Implementations;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone
{
    public class SpellZoneController : MechanismsTriggerBase, IInitializableSpellZoneController
    {
        private ITriggerOnSpellInteraction _triggerOnSpellInteraction;
        private List<ISpellType> _typesToTriggerOn;

        public void Initialize(List<ISpellType> typesToTriggerOn, ITriggerOnSpellInteraction triggerOnSpellInteraction,
            MechanismsTriggerBaseSetupData baseSetupData)
        {
            _typesToTriggerOn = typesToTriggerOn;
            _triggerOnSpellInteraction = triggerOnSpellInteraction;
            InitializeBase(baseSetupData);
        }

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
                TryInvokeTriggerEvent();
            }
        }
    }
}