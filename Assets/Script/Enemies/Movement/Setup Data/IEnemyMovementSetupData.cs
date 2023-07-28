using Interfaces;
using Pathfinding;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public interface IEnemyMovementSetupData
    {
        ICoroutineStarter CoroutineStarter { get; }
        Seeker Seeker { get; }
        Rigidbody Rigidbody { get; }
    }
}