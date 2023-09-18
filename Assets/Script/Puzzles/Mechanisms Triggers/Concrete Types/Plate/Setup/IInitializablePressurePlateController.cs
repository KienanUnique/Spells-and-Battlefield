using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Settings;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Setup
{
    public interface IInitializablePressurePlateController
    {
        public void Initialize(IIdentifier identifier, bool needTriggerOneTime, Transform plateTransform,
            IPlateSettings plateSettings, IColliderTrigger colliderTrigger);
    }
}