using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Settings.Ground_Layer_Mask;
using Pickable_Items.Settings;
using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;

namespace Pickable_Items.Setup
{
    public class PickableItemControllerBaseSetupData : IPickableItemControllerBaseSetupData
    {
        public PickableItemControllerBaseSetupData(bool setNeedFallDown,
            IStrategyForPickableController setStrategyForPickableController,
            IGroundLayerMaskSetting setGroundLayerMaskSetting, IPickableItemsSettings setPickableItemsSettings,
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
        public IGroundLayerMaskSetting SetGroundLayerMaskSetting { get; }
        public IPickableItemsSettings SetPickableItemsSettings { get; }
        public Transform SetVisualObjectTransform { get; }
        public GroundChecker SetGroundChecker { get; }
        public PickableItemsPickerTrigger SetPickerTrigger { get; }
        public Rigidbody SetRigidBody { get; }
    }
}