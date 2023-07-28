namespace Enemies.State_Machine
{
    public interface IInitializableEnemyStateMachineAI
    {
        void Initialize(IEnemyStateMachineControllable stateMachineControllable);
    }
}