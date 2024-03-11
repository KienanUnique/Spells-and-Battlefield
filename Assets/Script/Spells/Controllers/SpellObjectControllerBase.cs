using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Collision_Collider;
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
        [SerializeField] private SpellCollisionTriggerBase _spellCollisionTrigger;
        
        private IDataForSpellController _spellControllerData;
        private float _initializeTime;

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

        protected virtual void HandleTriggerEnter(Collider other)
        {
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

            foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.CheckContact(other);
            }

            HandleTriggerEnter(other);
        }
    }
}