namespace Enemies.State_Machine.Transition_Conditions
{
    public interface IInitializableTransitionEnemyAI
    {
        void Initialize(IEnemyStateMachineControllable stateMachineControllable);
    }
}