using Interfaces;
using Pathfinding;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public class EnemyMovementSetupData : IEnemyMovementSetupData
    {
        public EnemyMovementSetupData(Rigidbody rigidbody, Seeker seeker, ICoroutineStarter coroutineStarter)
        {
            Rigidbody = rigidbody;
            Seeker = seeker;
            CoroutineStarter = coroutineStarter;
        }

        public Rigidbody Rigidbody { get; }
        public Seeker Seeker { get; }
        public ICoroutineStarter CoroutineStarter { get; }
    }
}