using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction
{
    [RequireComponent(typeof(Collider))]
    public class TriggerOnSpellInteractionController : InitializableMonoBehaviourBase, ISpellInteractable,
        ITriggerOnSpellInteraction, IInitializableTriggerOnSpellInteractionController
    {
        private IIdHolder _idHolder;

        public void Initialize(IIdHolder idHolder)
        {
            _idHolder = idHolder;
            SetInitializedStatus();
        }

        public event Action<ISpellType> SpellTypeInteractionDetected;
        public int Id => _idHolder.Id;

        public void InteractAsSpellType(ISpellType spellType)
        {
            SpellTypeInteractionDetected?.Invoke(spellType);
        }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}