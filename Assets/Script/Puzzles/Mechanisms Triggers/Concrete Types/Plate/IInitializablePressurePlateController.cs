using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Triggers;
using Settings.Puzzles.Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Plate
{
    public interface IInitializablePressurePlateController
    {
        void Initialize(IIdentifier identifier, Transform plateTransform, PlateSettings plateSettings,
            IColliderTrigger colliderTrigger);
    }
}