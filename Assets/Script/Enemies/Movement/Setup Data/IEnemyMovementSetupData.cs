using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Target_Pathfinder;
using Enemies.Target_Selector_From_Triggers;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public interface IEnemyMovementSetupData
    {
        public ICoroutineStarter CoroutineStarter { get; }
        public Rigidbody Rigidbody { get; }
        public Collider Collider { get; }
        public IReadonlyEnemyTargetFromTriggersSelector TargetSelector { get; }
        public ITargetPathfinderForMovement TargetPathfinderForMovement { get; }
        public ISummoner Summoner { get; }
    }
}