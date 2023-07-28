namespace Enemies.State_Machine.States
{
    public interface IInitializableStateEnemyAI
    {
        void Initialize(IEnemyStateMachineControllable stateMachineControllable);
    }
}