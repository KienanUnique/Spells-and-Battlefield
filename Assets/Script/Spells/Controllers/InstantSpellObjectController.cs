using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Interfaces;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Factory;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class InstantSpellObjectController : InitializableMonoBehaviourBase,
        IInitializableInstantSpellController,
        ICoroutineStarter
    {
        private ICaster _caster;
        private float _initializeTime;
        private Rigidbody _rigidbody;
        private IDataForInstantSpellController _spellControllerData;
        private ISpellObjectsFactory _spellObjectsFactory;

        public void Initialize(IDataForInstantSpellController spellControllerData, ICaster caster,
            ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
            _caster = caster;
            _spellControllerData = spellControllerData;
            _rigidbody = GetComponent<Rigidbody>();
            _spellControllerData.Initialize(_rigidbody, caster, this);
            _initializeTime = Time.time;
            SetInitializedStatus();
            _spellControllerData.SpellObjectMovement.StartMoving();
        }

        private float TimePassedFromInitialize => Time.time - _initializeTime;

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        private void FixedUpdate()
        {
            if (CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.Initialized)
            {
                float commonTimePassedFromInitialize = TimePassedFromInitialize;
                foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
                {
                    spellApplier.CheckTime(commonTimePassedFromInitialize);
                }

                HandleSpellTriggerResponse(
                    _spellControllerData.SpellMainTrigger.CheckTime(commonTimePassedFromInitialize));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISpellInteractable otherAsSpellInteractable))
            {
                otherAsSpellInteractable.InteractAsSpellType(_spellControllerData.SpellType);
            }

            if (!other.isTrigger &&
                CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.Initialized)
            {
                foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
                {
                    spellApplier.CheckContact(other);
                }

                HandleSpellTriggerResponse(_spellControllerData.SpellMainTrigger.CheckContact(other));
            }
        }

        private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
        {
            if (response == SpellTriggerCheckStatusEnum.Finish)
            {
                HandleFinishSpell();
            }
        }

        private void HandleFinishSpell()
        {
            foreach (ISpellApplier spellApplier in _spellControllerData.SpellAppliers)
            {
                spellApplier.HandleRollbackableEffects();
            }

            foreach (IInformationAboutInstantSpell spell in _spellControllerData.NextSpellsOnFinish)
            {
                Transform spellTransform = _rigidbody.transform;
                _spellObjectsFactory.Create(spell.DataForController, spell.PrefabProvider, _caster,
                    spellTransform.position, spellTransform.rotation);
            }

            _spellControllerData.SpellObjectMovement.StopMoving();
            Destroy(gameObject);
        }
    }
}