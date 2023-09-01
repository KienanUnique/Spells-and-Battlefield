using Enemies.Target_Pathfinder;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public class EnemyMovementSetupData : IEnemyMovementSetupData
    {
        public EnemyMovementSetupData(Rigidbody rigidbody, IReadonlyEnemyTargetFromTriggersSelector targetSelector,
            ICoroutineStarter coroutineStarter, ITargetPathfinderForMovement targetPathfinderForMovement)
        {
            Rigidbody = rigidbody;
            TargetSelector = targetSelector;
            CoroutineStarter = coroutineStarter;
            TargetPathfinderForMovement = targetPathfinderForMovement;
        }

        public Rigidbody Rigidbody { get; }
        public IReadonlyEnemyTargetFromTriggersSelector TargetSelector { get; }
        public ITargetPathfinderForMovement TargetPathfinderForMovement { get; }
        public ICoroutineStarter CoroutineStarter { get; }
    }
}