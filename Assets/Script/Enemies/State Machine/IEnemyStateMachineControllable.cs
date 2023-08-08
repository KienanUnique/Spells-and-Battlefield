using Common.Event_Invoker_For_Action_Animations;
using Enemies.Character;
using Enemies.Look;
using Enemies.Movement;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Interfaces;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable : IEventInvokerForActionAnimations, IEnemyActionAnimationPlayer,
        IEnemyTargetsEffectsApplier, IEnemyMovementForStateMachine, IEnemyLookForStateMachine, ICharacterInformationProvider
    {
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; }
    }
}