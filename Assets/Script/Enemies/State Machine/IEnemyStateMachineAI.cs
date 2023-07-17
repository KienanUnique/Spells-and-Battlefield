using Enemies.Look;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineAI
    {
        void StartStateMachine(IEnemyStateMachineControllable stateMachineControllable, IEnemyLook enemyLook);

        void StopStateMachine();
    }
}