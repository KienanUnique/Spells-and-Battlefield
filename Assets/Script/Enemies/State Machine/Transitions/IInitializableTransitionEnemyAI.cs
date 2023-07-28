namespace Enemies.State_Machine.Transitions
{
    public interface IInitializableTransitionEnemyAI
    {
        void Initialize(IEnemyStateMachineControllable stateMachineControllable);
    }
}