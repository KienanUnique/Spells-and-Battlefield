using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Settings.Ground_Layer_Mask;
using Pickable_Items.Settings;
using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Pickable_Items.Setup
{
    public abstract class PickableItemControllerSetupBase<TController> : SetupMonoBehaviourBase,
        IPickableItemControllerSetupBase
    {
        [SerializeField] private PickableItemsPickerTrigger _pickerTrigger;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private Transform _visualObjectTransform;
        private TController _controllerToSetup;
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;
        private IGroundLayerMaskSetting _groundLayerMaskSetting;
        private IPickableItemsSettings _pickableItemsSettings;
        private Rigidbody _rigidbody;
        private IStrategyForPickableController _strategyForPickableController;
        private Vector3? _dropDirection;

        [Inject]
        private void GetDependencies(IGroundLayerMaskSetting groundLayerMaskSetting,
            IPickableItemsSettings pickableItemsSettings)
        {
            _groundLayerMaskSetting = groundLayerMaskSetting;
            _pickableItemsSettings = pickableItemsSettings;
        }

        protected abstract IEnumerable<IInitializable> AdditionalObjectsToWaitBeforeInitialization { get; }

        protected sealed override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(AdditionalObjectsToWaitBeforeInitialization)
            {
                _groundChecker, _externalDependenciesInitializationWaiter
            };

        public void SetBaseSetupData(IStrategyForPickableController strategyForPickableController)
        {
            _strategyForPickableController = strategyForPickableController;
            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        public void SetBaseSetupData(IStrategyForPickableController strategyForPickableController,
            Vector3 dropDirection)
        {
            _dropDirection = dropDirection;
            SetBaseSetupData(strategyForPickableController);
        }

        protected abstract void Initialize(IPickableItemControllerBaseSetupData baseSetupData,
            TController controllerToSetup);

        protected override void Initialize()
        {
            IPickableItemControllerBaseSetupData setupData;
            if (_dropDirection == null)
            {
                setupData = new PickableItemControllerBaseSetupData(_strategyForPickableController,
                    _groundLayerMaskSetting, _pickableItemsSettings, _visualObjectTransform, _groundChecker,
                    _pickerTrigger, _rigidbody);
            }
            else
            {
                setupData = new PickableItemControllerBaseSetupData(_strategyForPickableController,
                    _groundLayerMaskSetting, _pickableItemsSettings, _visualObjectTransform, _groundChecker,
                    _pickerTrigger, _rigidbody, _dropDirection.Value);
            }

            Initialize(setupData, _controllerToSetup);
        }

        protected override void Prepare()
        {
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);
            _rigidbody = GetComponent<Rigidbody>();
            _controllerToSetup = GetComponent<TController>();
        }
    }
}