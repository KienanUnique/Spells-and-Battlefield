using Common.Animator_Status_Controller;

namespace Enemies.State_Machine.States
{
    public interface IInitializableStateEnemyAI
    {
        void Initialize(IEnemyStateMachineControllable stateMachineControllable,
            IReadonlyAnimatorStatusChecker animatorStatusChecker);
    }
}