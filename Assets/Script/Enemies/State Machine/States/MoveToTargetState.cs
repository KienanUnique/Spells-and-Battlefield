using UnityEngine;

[RequireComponent(typeof(EnemyPathfinder))]
public class MoveToTargetState : State
{
    private EnemyPathfinder _enemyPathfinder;

    private void Awake()
    {
        _enemyPathfinder = GetComponent<EnemyPathfinder>();
    }

    public override void Enter(IPlayer target)
    {
        base.Enter(target);
        _enemyPathfinder.StartMovingToTarget(_target.MainTransform);
    }

    public override void Exit()
    {
        base.Exit();
        _enemyPathfinder.StopMovingToTarget();
    }
}