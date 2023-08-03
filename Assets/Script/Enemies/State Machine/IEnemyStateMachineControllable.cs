using Common.Event_Invoker_For_Action_Animations;
using Enemies.Character;
using Enemies.Look;
using Enemies.Movement;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable : IEventInvokerForActionAnimations, IEnemyActionAnimationPlayer,
        IEnemyTargetsEffectsApplier, IEnemyMovementForStateMachine, IEnemyLookForStateMachine
    {
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; }
    }
}