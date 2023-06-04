using Common.Abstract_Bases.Checkers;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Settings;
using UnityEngine;

namespace Pickable_Items.Setup
{
    public interface IPickableItemControllerBaseSetupData
    {
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