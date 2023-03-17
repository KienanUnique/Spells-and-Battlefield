using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(KnightController))]
public class KnightAttackState : State
{
    private KnightController _knightController;

    private void Awake()
    {
        _knightController = GetComponent<KnightController>();
    }

    public override void Enter(IPlayer target)
    {
        base.Enter(target);
        _knightController.StartSwordAttack(target.MainTransform);
    }

    public override void Exit()
    {
        base.Exit();
        _knightController.StopSwordAttack();
    }
}