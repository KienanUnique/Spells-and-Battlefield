using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone
{
    public class SpellZoneControllerSetup : MechanismsTriggerSetupBase
    {
        [SerializeField] private TriggerOnSpellInteractionController _triggerOnSpellInteraction;
        [SerializeField] private List<SpellTypeScriptableObject> _spellTypesToTriggerOn;
        private IInitializableSpellZoneController _controller;
        private List<ISpellType> _spellTypesToTriggerOnImplementations;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_triggerOnSpellInteraction};

        protected override void Initialize()
        {
            _controller.Initialize(_spellTypesToTriggerOnImplementations, _triggerOnSpellInteraction, BaseSetupData);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableSpellZoneController>();
            _spellTypesToTriggerOnImplementations = new List<ISpellType>();
            _spellTypesToTriggerOn.ForEach(spellType =>
                _spellTypesToTriggerOnImplementations.Add(spellType.GetImplementationObject()));
        }
    }
}