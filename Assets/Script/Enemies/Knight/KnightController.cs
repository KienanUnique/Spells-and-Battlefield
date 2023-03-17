using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KnightCharacter))]
[RequireComponent(typeof(KnightCharacter))]
public class KnightController : EnemyControllerBase
{
    [SerializeField] private KnightVisual _knightVisual;
    [SerializeField] private BoxColliderTargetSelector _swordTargetSelector;
    protected override EnemyVisualBase EnemyVisual => _knightVisual;
    private KnightCharacter _knightCharacter;

    protected override void Awake()
    {
        base.Awake();
        _knightCharacter = GetComponent<KnightCharacter>();
    }

    private void OnEnable()
    {
        _knightVisual.AttackWithSwordAnimationMomentStartEvent += OnAttackWithSwordAnimationMomentStartEvent;
    }

    private void OnDisable()
    {
        _knightVisual.AttackWithSwordAnimationMomentStartEvent -= OnAttackWithSwordAnimationMomentStartEvent;
    }

    public void StartSwordAttack(Transform _target)
    {
        _knightVisual.StartAttackWithSwordAnimation();
        _enemyMovement.StartMovingToTarget(_target);
    }

    public void StopSwordAttack()
    {
        _knightVisual.StopAttackWithSwordAnimation();
        _enemyMovement.StopCurrentAction();
    }

    public void OnAttackWithSwordAnimationMomentStartEvent()
    {
        var targets = _swordTargetSelector.GetTargetsInCollider();
        targets.RemoveAll(target => target.Id == Id);
        _knightCharacter.DamageTargetsWithSwordAttack(targets);
    }
}