using System.Collections;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Controllers.Data_For_Controller;
using Spells.Controllers.Data_For_Controller_Activation;
using Spells.Factory;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;
using Zenject;

namespace Spells.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SpellTargetsDetector))]
    public class
        SpellObjectControllerBase<TDataForActivation> :
            InitializableMonoBehaviourWithObjectPoolingBase<TDataForActivation>,
            ICoroutineStarter where TDataForActivation : IBaseDataForSpellControllerActivation
    {
        private float _activationTime;
        private ISpellTargetsDetector _targetsDetector;
        private Coroutine _checkTimeCoroutine;

        [Inject]
        private void GetDependencies(ISpellObjectsFactory spellObjectsFactory)
        {
            SpellObjectsFactory = spellObjectsFactory;
        }

        protected float TimePassedFromActivation => Time.time - _activationTime;
        protected ICaster Caster { get; private set; }
        protected Rigidbody SpellRigidbody { get; private set; }
        protected ISpellObjectsFactory SpellObjectsFactory { get; private set; }
        protected TDataForActivation DataForActivation { get; private set; }
        protected IReadonlyTransform CastPoint { get; private set; }
        private IBaseDataForSpellController SpellControllerData => DataForActivation.BaseDataForSpellController;

        public override void Activate(TDataForActivation dataForActivation)
        {
            base.Activate(dataForActivation);
            dataForActivation.Initialize(SpellRigidbody, this, _targetsDetector);
            DataForActivation = dataForActivation;
            CastPoint = dataForActivation.CastPoint;
            Caster = dataForActivation.Caster;
            _activationTime = Time.time;
            _checkTimeCoroutine = StartCoroutine(CheckTimeContinuously());
            gameObject.SetActive(true);
        }

        protected virtual void CheckTime(float timePassedFromActivation)
        {
            foreach (ISpellApplier spellApplier in SpellControllerData.SpellAppliers)
            {
                spellApplier.CheckTime(timePassedFromActivation);
            }
        }

        protected virtual void HandleTriggerEnter(Collider other)
        {
        }

        protected virtual void HandleFinishSpell()
        {
            foreach (ISpellApplier spellApplier in SpellControllerData.SpellAppliers)
            {
                spellApplier.HandleRollbackableEffects();
            }

            SpellControllerData.SpellObjectMovement.StopMoving();
            Deactivate();
        }

        protected override void Deactivate()
        {
            base.Deactivate();
            if (_checkTimeCoroutine != null)
            {
                StopCoroutine(_checkTimeCoroutine);
                _checkTimeCoroutine = null;
            }

            gameObject.SetActive(false);
            DataForActivation = default;
            CastPoint = null;
            Caster = null;
            _activationTime = 0;
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
            SpellControllerData.SpellObjectMovement.StartMoving();
        }

        protected void InitializeBase()
        {
            SpellRigidbody = GetComponent<Rigidbody>();
            _targetsDetector = GetComponent<SpellTargetsDetector>();
            gameObject.SetActive(false);
        }

        private IEnumerator CheckTimeContinuously()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                yield return waitForFixedUpdate;
                CheckTime(TimePassedFromActivation);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsUsed)
            {
                return;
            }

            if (other.TryGetComponent(out ISpellInteractable otherAsSpellInteractable))
            {
                otherAsSpellInteractable.InteractAsSpellType(SpellControllerData.SpellType);
            }

            if (other.isTrigger ||
                CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized)
            {
                return;
            }

            foreach (ISpellApplier spellApplier in SpellControllerData.SpellAppliers)
            {
                spellApplier.CheckContact(other);
            }

            HandleTriggerEnter(other);
        }
    }
}