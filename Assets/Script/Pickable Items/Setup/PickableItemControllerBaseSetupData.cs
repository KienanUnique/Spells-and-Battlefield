using Common.Abstract_Bases.Checkers;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Settings;
using UnityEngine;

namespace Pickable_Items.Setup
{
    public class PickableItemControllerBaseSetupData : IPickableItemControllerBaseSetupData
    {
        public PickableItemControllerBaseSetupData(bool setNeedFallDown,
            IStrategyForPickableController setStrategyForPickableController,
            GroundLayerMaskSetting setGroundLayerMaskSetting, PickableItemsSettings setPickableItemsSettings,
            Transform setVisualObjectTransform, GroundChecker setGroundChecker,
            PickableItemsPickerTrigger setPickerTrigger, Rigidbody setRigidBody)
        {
            SetNeedFallDown = setNeedFallDown;
            SetStrategyForPickableController = setStrategyForPickableController;
            SetGroundLayerMaskSetting = setGroundLayerMaskSetting;
            SetPickableItemsSettings = setPickableItemsSettings;
            SetVisualObjectTransform = setVisualObjectTransform;
            SetGroundChecker = setGroundChecker;
            SetPickerTrigger = setPickerTrigger;
            SetRigidBody = setRigidBody;
        }

        public bool SetNeedFallDown { get; }
        public IStrategyForPickableController SetStrategyForPickableController { get; }
        public GroundLayerMaskSetting SetGroundLayerMaskSetting { get; }
        public PickableItemsSettings SetPickableItemsSettings { get; }
        public Transform SetVisualObjectTransform { get; }
        public GroundChecker SetGroundChecker { get; }
        public PickableItemsPickerTrigger SetPickerTrigger { get; }
        public Rigidbody SetRigidBody { get; }
    }
}