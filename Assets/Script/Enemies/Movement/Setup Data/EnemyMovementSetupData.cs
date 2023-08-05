using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using Pathfinding;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public class EnemyMovementSetupData : IEnemyMovementSetupData
    {
        public EnemyMovementSetupData(Rigidbody rigidbody, IReadonlyEnemyTargetFromTriggersSelector targetSelector,
            Seeker seeker, ICoroutineStarter coroutineStarter)
        {
            Rigidbody = rigidbody;
            TargetSelector = targetSelector;
            Seeker = seeker;
            CoroutineStarter = coroutineStarter;
        }

        public Rigidbody Rigidbody { get; }
        public IReadonlyEnemyTargetFromTriggersSelector TargetSelector { get; }
        public Seeker Seeker { get; }
        public ICoroutineStarter CoroutineStarter { get; }
    }
}