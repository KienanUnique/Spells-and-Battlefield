using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpellObjectController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private ISpellMovement _spellMovement;
        private ISpellInteractable _casterCharacter;
        private List<SpellBase> _nextSpellsOnFinish;
        private List<ISpellApplier> _spellAppliers;
        private ISpellTrigger _spellMainTrigger;
        private float _initializeTime;
        private SpellControllerStatus _controllerStatus = SpellControllerStatus.NonInitialized;
        private float TimePassedFromInitialize => Time.time - _initializeTime;

        private enum SpellControllerStatus
        {
            NonInitialized,
            Active,
            Finished
        }

#nullable enable
        public void Initialize(ISpellMovement spellMovement, List<SpellBase> nextSpellsOnFinish,
            List<ISpellApplier> spellAppliers, ISpellTrigger spellMainTrigger,
            Transform? casterTransform, ISpellInteractable casterCharacter)
        {
            _spellMovement = spellMovement;
            _spellAppliers = spellAppliers;
            _spellMainTrigger = spellMainTrigger;

            var spellImplementations = new List<ISpellImplementation>()
            {
                _spellMovement,
                _spellMainTrigger
            };
            spellImplementations.AddRange(_spellAppliers);

            spellImplementations.ForEach(spellImplementation =>
                spellImplementation.Initialize(_rigidbody, casterTransform, casterCharacter));

            _casterCharacter = casterCharacter;
            _nextSpellsOnFinish = nextSpellsOnFinish;

            _initializeTime = Time.time;
            _controllerStatus = SpellControllerStatus.Active;
        }
#nullable disable

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_controllerStatus == SpellControllerStatus.Active)
            {
                _spellMovement.UpdatePosition();

                float commonTimePassedFromInitialize = TimePassedFromInitialize;
                _spellAppliers.ForEach(spellApplier => spellApplier.CheckTime(commonTimePassedFromInitialize));
                HandleSpellTriggerResponse(_spellMainTrigger.CheckTime(commonTimePassedFromInitialize));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger && _controllerStatus == SpellControllerStatus.Active)
            {
                _spellAppliers.ForEach(spellApplier => spellApplier.CheckContact(other));
                HandleSpellTriggerResponse(_spellMainTrigger.CheckContact(other));
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
            _spellAppliers.ForEach(applier => applier.HandleRollbackableEffects());
            _nextSpellsOnFinish.ForEach(spell =>
            {
                var spellTransform = transform;
                spell.Cast(spellTransform.position, spellTransform.rotation, spellTransform, _casterCharacter);
            });
            Destroy(this.gameObject);
        }
    }
}