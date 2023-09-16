using Spells.Factory;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class SingleSpellObjectController : MonoBehaviour, ISpellObjectController
    {
        private ICaster _caster;
        private SpellControllerStatus _controllerStatus = SpellControllerStatus.NonInitialized;
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
            _spellData.Initialize(_rigidbody, caster);

            _initializeTime = Time.time;
            _controllerStatus = SpellControllerStatus.Active;
        }

        private enum SpellControllerStatus
        {
            NonInitialized, Active, Finished
        }

        private float TimePassedFromInitialize => Time.time - _initializeTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_controllerStatus == SpellControllerStatus.Active)
            {
                _spellData.SpellObjectMovement.UpdatePosition();

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

            if (!other.isTrigger && _controllerStatus == SpellControllerStatus.Active)
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
            _controllerStatus = SpellControllerStatus.Finished;
            _spellData.SpellAppliers.ForEach(applier => applier.HandleRollbackableEffects());
            _spellData.NextSpellsOnFinish.ForEach(spell =>
            {
                Transform spellTransform = _rigidbody.transform;
                _spellObjectsFactory.Create(spell.SpellDataForSpellController, spell.SpellPrefabProvider, _caster,
                    spellTransform.position, spellTransform.rotation);
            });
            Destroy(gameObject);
        }
    }
}