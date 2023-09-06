using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Settings.Ground_Layer_Mask;
using Pickable_Items.Settings;
using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;

namespace Pickable_Items.Setup
{
    public interface IPickableItemControllerBaseSetupData
    {
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