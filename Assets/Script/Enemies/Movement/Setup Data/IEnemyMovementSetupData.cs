using Common.Interfaces;
using Enemies.Target_Pathfinder;
using Enemies.Target_Selector_From_Triggers;
using UnityEngine;

namespace Enemies.Movement.Setup_Data
{
    public interface IEnemyMovementSetupData
    {
        ICoroutineStarter CoroutineStarter { get; }
        Rigidbody Rigidbody { get; }
        IReadonlyEnemyTargetFromTriggersSelector TargetSelector { get; }
        ITargetPathfinderForMovement TargetPathfinderForMovement { get; }
    }
}