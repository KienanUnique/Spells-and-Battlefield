using Common.Abstract_Bases.Character;
using General_Settings_in_Scriptable_Objects;
using Settings;
using UnityEngine;
using Zenject;

namespace Enemies.Knight
{
    public class KnightController : EnemyControllerBase
    {
        [SerializeField] private KnightVisual _knightVisual;
        [SerializeField] private BoxColliderTargetSelector _swordTargetSelector;
        protected override EnemyVisualBase EnemyVisual => _knightVisual;
        protected override IEnemySettings EnemySettings => _knightSettings;
        protected override CharacterBase Character => _knightCharacter;
        private KnightCharacter _knightCharacter;
        private KnightSettings _knightSettings;

        [Inject]
        private void Construct(KnightSettings knightSettings)
        {
            _knightSettings = knightSettings;
        }

        public void StartSwordAttack(Transform target)
        {
            _knightVisual.StartAttackWithSwordAnimation();
            _enemyMovement.StartMovingToTarget(target);
        }

        public void StopSwordAttack()
        {
            _knightVisual.StopAttackWithSwordAnimation();
        }

        protected override void Awake()
        {
            _knightCharacter = new KnightCharacter(this, _knightSettings.KnightCharacterSettings);
            base.Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _knightCharacter.Enable();
            _knightVisual.AttackWithSwordAnimationMomentStart += HandleAttackWithSwordAnimationMomentStart;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _knightCharacter.Disable();
            _knightVisual.AttackWithSwordAnimationMomentStart -= HandleAttackWithSwordAnimationMomentStart;
        }


        private void HandleAttackWithSwordAnimationMomentStart()
        {
            var targets = _swordTargetSelector.GetTargetsInCollider();
            targets.RemoveAll(target => target.Id == Id);
            _knightCharacter.TryDamageTargetsWithSwordAttack(targets);
        }
    }
}