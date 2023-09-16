using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Target_Pathfinder;
using Enemies.Target_Selector_From_Triggers;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public class EnemyMovementSetupData : IEnemyMovementSetupData
    {
        public EnemyMovementSetupData(Rigidbody rigidbody, IReadonlyEnemyTargetFromTriggersSelector targetSelector,
            ICoroutineStarter coroutineStarter, ITargetPathfinderForMovement targetPathfinderForMovement,
            ISummoner summoner)
        {
            Rigidbody = rigidbody;
            TargetSelector = targetSelector;
            CoroutineStarter = coroutineStarter;
            TargetPathfinderForMovement = targetPathfinderForMovement;
            Summoner = summoner;
        }

        public Rigidbody Rigidbody { get; }
        public IReadonlyEnemyTargetFromTriggersSelector TargetSelector { get; }
        public ITargetPathfinderForMovement TargetPathfinderForMovement { get; }
        public ISummoner Summoner { get; }
        public ICoroutineStarter CoroutineStarter { get; }
    }
}