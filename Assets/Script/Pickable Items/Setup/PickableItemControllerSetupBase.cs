using Common;
using Common.Abstract_Bases.Checkers;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Settings;
using UnityEngine;
using Zenject;

namespace Pickable_Items.Setup
{
    public abstract class PickableItemControllerSetupBase<TController> : MonoBehaviour, IPickableItemStrategySettable
    {
        [SerializeField] private PickableItemsPickerTrigger _pickerTrigger;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private Transform _visualObjectTransform;
        private PickableItemsSettings _pickableItemsSettings;
        private GroundLayerMaskSetting _groundLayerMaskSetting;
        private IStrategyForPickableController _strategyForPickableController;
        private Rigidbody _rigidbody;
        private bool _needFallDown;

        private ValueWithReactionOnChange<bool> _isBaseReadyForSetup;
        protected ValueWithReactionOnChange<bool> _isChildReadyForSetup;

        [Inject]
        private void Construct(GroundLayerMaskSetting groundLayerMaskSetting,
            PickableItemsSettings pickableItemsSettings)
        {
            _groundLayerMaskSetting = groundLayerMaskSetting;
            _pickableItemsSettings = pickableItemsSettings;
        }

        public void SetStrategyForPickableController(IStrategyForPickableController strategyForPickableController,
            bool needFallDown)
        {
            _strategyForPickableController = strategyForPickableController;
            _needFallDown = needFallDown;
            _isBaseReadyForSetup.Value = true;
        }

        protected abstract void SetupConcreteController(IPickableItemControllerBaseSetupData baseSetupData,
            TController controllerToSetup);

        private void Awake()
        {
            _isChildReadyForSetup = new ValueWithReactionOnChange<bool>(false);
            _isBaseReadyForSetup = new ValueWithReactionOnChange<bool>(false);
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _isChildReadyForSetup.AfterValueChanged += SetupControllerIfReady;
        }

        private void OnDisable()
        {
            _isChildReadyForSetup.AfterValueChanged -= SetupControllerIfReady;
        }

        private void SetupControllerIfReady(bool unnecessaryValue)
        {
            if (_isChildReadyForSetup.Value && _isBaseReadyForSetup.Value)
            {
                Setup();
            }
        }

        private void Setup()
        {
            var controllerToSetup = GetComponent<TController>();
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

            SetupConcreteController(setupData, controllerToSetup);
        }
    }
}