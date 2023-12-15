using Common.Abstract_Bases.Character;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Character;
using Enemies.Look;
using Enemies.Movement;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable : IEnemyActionAnimationPlayer,
        IEnemyTargetsEffectsApplier,
        IEnemyMovementForStateMachine,
        IEnemyLookForStateMachine,
        ICharacterInformationProvider
    {
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; }
        public ISummoner Summoner { get; }
    }
}