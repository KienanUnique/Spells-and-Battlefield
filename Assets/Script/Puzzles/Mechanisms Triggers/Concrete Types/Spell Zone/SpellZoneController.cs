using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction;
using Spells.Implementations_Interfaces.Implementations;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone
{
    public class SpellZoneController : MechanismsTriggerBase, IInitializableSpellZoneController
    {
        private ITriggerOnSpellInteraction _triggerOnSpellInteraction;
        private List<ISpellType> _typesToTriggerOn;
        private bool _needTriggerOneTime;

        public void Initialize(List<ISpellType> typesToTriggerOn, bool needTriggerOneTime,
            ITriggerOnSpellInteraction triggerOnSpellInteraction)
        {
            _typesToTriggerOn = typesToTriggerOn;
            _triggerOnSpellInteraction = triggerOnSpellInteraction;
            _needTriggerOneTime = needTriggerOneTime;
            SetInitializedStatus();
        }

        protected override bool NeedTriggerOneTime => _needTriggerOneTime;

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