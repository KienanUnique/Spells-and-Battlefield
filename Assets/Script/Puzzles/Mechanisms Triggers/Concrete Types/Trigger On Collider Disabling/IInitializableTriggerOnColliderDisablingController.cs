using Common.Collider_With_Disabling;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_On_Collider_Disabling
{
    public interface IInitializableTriggerOnColliderDisablingController
    {
        void Initialize(IReadonlyColliderWithDisabling collider, MechanismsTriggerBaseSetupData setupData);
    }
}