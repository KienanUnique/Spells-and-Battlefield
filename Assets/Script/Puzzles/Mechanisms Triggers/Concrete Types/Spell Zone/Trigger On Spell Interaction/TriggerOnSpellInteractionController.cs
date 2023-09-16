using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Id_Holder;
using Spells;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction
{
    [RequireComponent(typeof(Collider))]
    public class TriggerOnSpellInteractionController : InitializableMonoBehaviourBase,
        ISpellInteractable,
        ITriggerOnSpellInteraction,
        IInitializableTriggerOnSpellInteractionController
    {
        private IIdHolder _idHolder;

        public void Initialize(IIdHolder idHolder)
        {
            _idHolder = idHolder;
            SetInitializedStatus();
        }

        public event Action<ISpellType> SpellTypeInteractionDetected;

        public int Id => _idHolder.Id;

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void InteractAsSpellType(ISpellType spellType)
        {
            SpellTypeInteractionDetected?.Invoke(spellType);
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}