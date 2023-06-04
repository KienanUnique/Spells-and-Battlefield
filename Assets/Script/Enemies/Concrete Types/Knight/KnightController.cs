using System.Collections.Generic;
using Enemies.Attack_Target_Selector;
using Enemies.Concrete_Types.Knight.Character;
using Enemies.Concrete_Types.Knight.Setup;
using Enemies.Concrete_Types.Knight.Visual;
using Enemies.Controller;
using Enemies.Setup;
using Enemies.Visual;
using General_Settings_in_Scriptable_Objects;
using Interfaces;
using Settings;
using UnityEngine;

namespace Enemies.Concrete_Types.Knight
{
    [RequireComponent(typeof(KnightControllerSetup))]
    public class KnightController : EnemyControllerBase, IInitializableKnightController
    {
        private IKnightVisual _knightVisual;
        private IAttackTargetSelectorFromZone _swordTargetSelector;
        private IKnightCharacter _knightCharacter;
        private KnightSettings _knightSettings;

        public void Initialize(IEnemyBaseSetupData baseSetupData, IKnightVisual knightVisual,
            IAttackTargetSelectorFromZone swordTargetSelector, IKnightCharacter knightCharacter,
            KnightSettings knightSettings)
        {
            _knightVisual = knightVisual;
            _swordTargetSelector = swordTargetSelector;
            _knightCharacter = knightCharacter;
            _knightSettings = knightSettings;

            InitializeBase(baseSetupData);
        }

        protected override IEnemyVisualBase EnemyVisual => _knightVisual;
        protected override IEnemyCharacter Character => _knightCharacter;


        public void StartSwordAttack(IEnemyTarget target)
        {
            _knightVisual.StartAttackWithSwordAnimation();
            _enemyMovement.StartFollowingPosition(target.MainTransform);
        }

        public void StopSwordAttack()
        {
            _knightVisual.StopAttackWithSwordAnimation();
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _knightVisual.AttackWithSwordAnimationMomentStart += HandleAttackWithSwordAnimationMomentStart;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _knightVisual.AttackWithSwordAnimationMomentStart -= HandleAttackWithSwordAnimationMomentStart;
        }


        private void HandleAttackWithSwordAnimationMomentStart()
        {
            var targets = new List<IEnemyTarget>(_swordTargetSelector.GetTargetsInCollider());
            targets.RemoveAll(target => target.Id == Id);
            _knightCharacter.TryDamageTargetsWithSwordAttack(targets);
        }
    }
}