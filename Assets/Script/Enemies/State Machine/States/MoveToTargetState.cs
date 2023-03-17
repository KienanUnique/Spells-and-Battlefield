using UnityEngine;

[RequireComponent(typeof(EnemyControllerBase))]
public class MoveToTargetState : State
{
    private EnemyControllerBase _enemyPathfinder;

    private void Awake()
    {
        _enemyPathfinder = GetComponent<EnemyControllerBase>();
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