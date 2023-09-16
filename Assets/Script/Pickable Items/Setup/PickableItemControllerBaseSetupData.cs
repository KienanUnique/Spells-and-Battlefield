using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Settings.Ground_Layer_Mask;
using Pickable_Items.Settings;
using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;

namespace Pickable_Items.Setup
{
    public class PickableItemControllerBaseSetupData : IPickableItemControllerBaseSetupData
    {
        private readonly Vector3? _dropDirection;

        public PickableItemControllerBaseSetupData(IStrategyForPickableController setStrategyForPickableController,
            IGroundLayerMaskSetting setGroundLayerMaskSetting, IPickableItemsSettings setPickableItemsSettings,
            Transform setVisualObjectTransform, GroundChecker setGroundChecker,
            PickableItemsPickerTrigger setPickerTrigger, Rigidbody setRigidBody)
        {
            SetStrategyForPickableController = setStrategyForPickableController;
            SetGroundLayerMaskSetting = setGroundLayerMaskSetting;
            SetPickableItemsSettings = setPickableItemsSettings;
            SetVisualObjectTransform = setVisualObjectTransform;
            SetGroundChecker = setGroundChecker;
            SetPickerTrigger = setPickerTrigger;
            SetRigidBody = setRigidBody;
        }

        public PickableItemControllerBaseSetupData(IStrategyForPickableController setStrategyForPickableController,
            IGroundLayerMaskSetting setGroundLayerMaskSetting, IPickableItemsSettings setPickableItemsSettings,
            Transform setVisualObjectTransform, GroundChecker setGroundChecker,
            PickableItemsPickerTrigger setPickerTrigger, Rigidbody setRigidBody, Vector3 dropDirection) : this(
            setStrategyForPickableController, setGroundLayerMaskSetting, setPickableItemsSettings,
            setVisualObjectTransform, setGroundChecker, setPickerTrigger, setRigidBody)
        {
            _dropDirection = dropDirection;
        }

        public IStrategyForPickableController SetStrategyForPickableController { get; }
        public IGroundLayerMaskSetting SetGroundLayerMaskSetting { get; }
        public IPickableItemsSettings SetPickableItemsSettings { get; }
        public Transform SetVisualObjectTransform { get; }
        public GroundChecker SetGroundChecker { get; }
        public PickableItemsPickerTrigger SetPickerTrigger { get; }
        public Rigidbody SetRigidBody { get; }

        public bool TryGetDropDirection(out Vector3? dropDirection)
        {
            dropDirection = _dropDirection;
            return _dropDirection != null;
        }
    }
}