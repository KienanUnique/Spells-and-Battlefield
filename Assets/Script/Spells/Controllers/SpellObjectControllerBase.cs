﻿using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Collision_Collider;
using Spells.Controllers.Data_For_Controller;
using Spells.Data_For_Spell_Implementation;
using Spells.Factory;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Controllers
{
    public class SpellObjectControllerBase : InitializableMonoBehaviourBase, ICoroutineStarter
    {
        [SerializeField] private SpellCollisionTriggerBase _spellCollisionTrigger;
        [SerializeField] private SpellTargetsDetector _spellTargetsDetector;

        private IDataForSpellController _spellControllerData;
        private float _initializeTime;

        protected float TimePassedFromInitialize => Time.time - _initializeTime;
        protected ICaster Caster { get; private set; }
        protected ISpellObjectsFactory SpellObjectsFactory { get; private set; }
        protected IReadonlyTransform CastPoint { get; private set; }

        protected virtual void FixedUpdate()
        {
            if (CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized)
            {
                return;
            }

            var commonTimePassedFromInitialize = TimePassedFromInitialize;
            foreach (var spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.CheckTime(commonTimePassedFromInitialize);
            }
        }

        protected virtual void HandleTriggerEnter(Collider other)
        {
        }

        protected virtual void HandleFinishSpell()
        {
            foreach (var spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.HandleRollbackableEffects();
            }

            _spellControllerData.SpellObjectMovement.StopMoving();
            Destroy(gameObject);
        }

        protected override void SubscribeOnEvents()
        {
            _spellCollisionTrigger.TriggerEntered += OnTriggerEntered;
        }

        protected override void UnsubscribeFromEvents()
        {
            _spellCollisionTrigger.TriggerEntered -= OnTriggerEntered;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _spellControllerData.SpellObjectMovement.StartMoving();
        }

        protected void InitializeBase(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IDataForSpellController spellControllerData, IReadonlyTransform castPoint)
        {
            TryGetComponent(out Rigidbody spellRigidbody);
            _spellControllerData = spellControllerData;
            CastPoint = castPoint;
            _spellControllerData.Initialize(new DataForSpellImplementation(spellRigidbody, transform, caster, this,
                castPoint, _spellTargetsDetector));
            SpellObjectsFactory = spellObjectsFactory;
            Caster = caster;
            _initializeTime = Time.time;
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.isTrigger ||
                CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized)
            {
                return;
            }

            if (other.TryGetComponent(out ISpellInteractable otherAsSpellInteractable))
            {
                otherAsSpellInteractable.InteractAsSpellType(_spellControllerData.SpellType);
            }

            foreach (var spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.CheckContact(other);
            }

            HandleTriggerEnter(other);
        }
    }
}