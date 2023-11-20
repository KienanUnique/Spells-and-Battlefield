using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Controllers.Data_For_Controller;
using Spells.Data_For_Spell_Implementation;
using Spells.Factory;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SpellTargetsDetector))]
    public class SpellObjectControllerBase : InitializableMonoBehaviourBase, ICoroutineStarter
    {
        private IDataForSpellController _spellControllerData;
        private float _initializeTime;

        protected void InitializeBase(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IDataForSpellController spellControllerData, IReadonlyTransform castPoint)
        {
            SpellRigidbody = GetComponent<Rigidbody>();
            var targetsDetector = GetComponent<SpellTargetsDetector>();
            _spellControllerData = spellControllerData;
            CastPoint = castPoint;
            _spellControllerData.Initialize(new DataForSpellImplementation(SpellRigidbody, caster, this, castPoint,
                targetsDetector));
            SpellObjectsFactory = spellObjectsFactory;
            Caster = caster;
            _initializeTime = Time.time;
        }

        protected float TimePassedFromInitialize => Time.time - _initializeTime;
        protected ICaster Caster { get; private set; }
        protected Rigidbody SpellRigidbody { get; private set; }
        protected ISpellObjectsFactory SpellObjectsFactory { get; private set; }
        protected IReadonlyTransform CastPoint { get; private set; }

        protected virtual void FixedUpdate()
        {
            if (CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized)
            {
                return;
            }

            float commonTimePassedFromInitialize = TimePassedFromInitialize;
            foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.CheckTime(commonTimePassedFromInitialize);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISpellInteractable otherAsSpellInteractable))
            {
                otherAsSpellInteractable.InteractAsSpellType(_spellControllerData.SpellType);
            }

            if (other.isTrigger ||
                CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized)
            {
                return;
            }

            foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.CheckContact(other);
            }

            HandleTriggerEnter(other);
        }

        protected virtual void HandleTriggerEnter(Collider other)
        {
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _spellControllerData.SpellObjectMovement.StartMoving();
        }

        protected virtual void HandleFinishSpell()
        {
            foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.HandleRollbackableEffects();
            }

            _spellControllerData.SpellObjectMovement.StopMoving();
            Destroy(gameObject);
        }
    }
}