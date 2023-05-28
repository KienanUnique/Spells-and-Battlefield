namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineAI
    {
        void StartStateMachine(IEnemyStateMachineControllable stateMachineControllable);
        void StopStateMachine();
    }
}