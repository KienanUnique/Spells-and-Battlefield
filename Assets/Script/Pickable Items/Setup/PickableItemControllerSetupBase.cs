using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Checkers;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Settings;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Pickable_Items.Setup
{
    public abstract class PickableItemControllerSetupBase<TController> : SetupMonoBehaviourBase,
        IPickableItemStrategySettable
    {
        [SerializeField] private PickableItemsPickerTrigger _pickerTrigger;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private Transform _visualObjectTransform;
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;
        private PickableItemsSettings _pickableItemsSettings;
        private GroundLayerMaskSetting _groundLayerMaskSetting;
        private IStrategyForPickableController _strategyForPickableController;
        private Rigidbody _rigidbody;
        private bool _needFallDown;
        private TController _controllerToSetup;

        [Inject]
        private void Construct(GroundLayerMaskSetting groundLayerMaskSetting,
            PickableItemsSettings pickableItemsSettings)
        {
            _groundLayerMaskSetting = groundLayerMaskSetting;
            _pickableItemsSettings = pickableItemsSettings;
        }

        protected abstract IEnumerable<IInitializable> AdditionalObjectsToWaitBeforeInitialization { get; }

        protected sealed override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(AdditionalObjectsToWaitBeforeInitialization)
            {
                _groundChecker,
                _externalDependenciesInitializationWaiter
            };

        public void SetStrategyForPickableController(IStrategyForPickableController strategyForPickableController,
            bool needFallDown)
        {
            _strategyForPickableController = strategyForPickableController;
            _needFallDown = needFallDown;
            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        protected abstract void Initialize(IPickableItemControllerBaseSetupData baseSetupData,
            TController controllerToSetup);

        protected override void Prepare()
        {
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);
            _rigidbody = GetComponent<Rigidbody>();
            _controllerToSetup = GetComponent<TController>();
        }

        protected override void Initialize()
        {
            var setupData = new PickableItemControllerBaseSetupData(
                _needFallDown,
                _strategyForPickableController,
                _groundLayerMaskSetting,
                _pickableItemsSettings,
                _visualObjectTransform,
                _groundChecker,
                _pickerTrigger,
                _rigidbody
            );
            Initialize(setupData, _controllerToSetup);
        }
    }
}