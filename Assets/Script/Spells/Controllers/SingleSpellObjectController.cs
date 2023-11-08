using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Interfaces;
using Spells.Factory;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class SingleSpellObjectController : InitializableMonoBehaviourBase,
        IInitializableSpellObjectController,
        ICoroutineStarter
    {
        private ICaster _caster;
        private float _initializeTime;
        private Rigidbody _rigidbody;
        private ISpellDataForSpellController _spellData;
        private ISpellObjectsFactory _spellObjectsFactory;

        public void Initialize(ISpellDataForSpellController spellData, ICaster caster,
            ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
            _caster = caster;
            _spellData = spellData;
            _rigidbody = GetComponent<Rigidbody>();
            _spellData.Initialize(_rigidbody, caster, this);
            _initializeTime = Time.time;
            SetInitializedStatus();
            _spellData.SpellObjectMovement.StartMoving();
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
                _spellData.SpellAppliers.ForEach(spellApplier =>
                    spellApplier.CheckTime(commonTimePassedFromInitialize));
                HandleSpellTriggerResponse(_spellData.SpellMainTrigger.CheckTime(commonTimePassedFromInitialize));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISpellInteractable otherAsSpellInteractable))
            {
                otherAsSpellInteractable.InteractAsSpellType(_spellData.SpellType);
            }

            if (!other.isTrigger &&
                CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.Initialized)
            {
                _spellData.SpellAppliers.ForEach(spellApplier => spellApplier.CheckContact(other));
                HandleSpellTriggerResponse(_spellData.SpellMainTrigger.CheckContact(other));
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
            _spellData.SpellAppliers.ForEach(applier => applier.HandleRollbackableEffects());
            _spellData.NextSpellsOnFinish.ForEach(spell =>
            {
                Transform spellTransform = _rigidbody.transform;
                _spellObjectsFactory.Create(spell.SpellDataForSpellController, spell.SpellPrefabProvider, _caster,
                    spellTransform.position, spellTransform.rotation);
            });
            _spellData.SpellObjectMovement.StopMoving();
            Destroy(gameObject);
        }
    }
}