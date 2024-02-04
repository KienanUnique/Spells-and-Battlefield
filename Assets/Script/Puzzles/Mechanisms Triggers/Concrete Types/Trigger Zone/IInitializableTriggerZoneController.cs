using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Identifiers;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_Zone
{
    public interface IInitializableTriggerZoneController
    {
        public void Initialize(IIdentifier identifier, IColliderTrigger colliderTrigger, TriggerEventType eventType,
            MechanismsTriggerBaseSetupData baseSetupData);
    }
}