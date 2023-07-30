using System;
using Common.Readonly_Transform;
using Enemies.Controller;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Interfaces;
using Spells.Factory;
using Spells.Spell;
using UnityEngine;
using Zenject;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    public class UseSpellsStateEnemyAI : StateEnemyAI, ICoroutineStarter
    {
        [SerializeField] private SpellsWithCooldownSelector _spellsSelector;
        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private ReadonlyTransformGetter _spellSpawnObjectReadonlyTransformGetter;
        private ILookPointCalculator _currentLookPointCalculator;
        private ISpell _cachedSpell;
        private bool _isWaitingForAnimationFinish = false;
        private ISpellObjectsFactory _spellObjectsFactory;
        private IReadonlyTransform _spellSpawnObject;
        private IReadonlyTransform _cachedThisTransform;

        [Inject]
        private void Construct(ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
        }

        public override event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public override ILookPointCalculator LookPointCalculator => _currentLookPointCalculator;

        protected override void SpecialEnterAction()
        {
            SubscribeOnLocalEvents();
            Debug.Log($"._spellsSelector.CanUseSpell {_spellsSelector.CanUseSpell} && !_isWaitingForAnimationFinish {!_isWaitingForAnimationFinish}");
            if (_spellsSelector.CanUseSpell && !_isWaitingForAnimationFinish)
            {
                TryCastSelectedSpell();
            }
        }

        protected override void SpecialExitAction()
        {
            UnsubscribeFromLocalEvents();
        }

        protected override void Awake()
        {
            base.Awake();
            _currentLookPointCalculator = new FollowTargetLookPointCalculator();
            _spellSpawnObject = _spellSpawnObjectReadonlyTransformGetter.ReadonlyTransform;
            _cachedThisTransform = new ReadonlyTransform(transform);
            _spellsSelector.Initialize(this);
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            if (IsActivated)
            {
                SubscribeOnLocalEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            UnsubscribeFromLocalEvents();
        }

        private void SubscribeOnLocalEvents()
        {
            _spellsSelector.CanUseSpellsAgain += OnCanUseSpellsAgain;
            StateMachineControllable.AnimationUseActionMomentTrigger += OnAnimationUseActionMomentTrigger;
        }

        private void UnsubscribeFromLocalEvents()
        {
            _spellsSelector.CanUseSpellsAgain -= OnCanUseSpellsAgain;
            StateMachineControllable.AnimationUseActionMomentTrigger -= OnAnimationUseActionMomentTrigger;
        }

        private void OnAnimationUseActionMomentTrigger()
        {
            CreateSelectedSpell(_cachedThisTransform.Rotation);

            if (_spellsSelector.CanUseSpell) return;
            _currentLookPointCalculator = new FollowTargetLookPointCalculator();
            NeedChangeLookPointCalculator?.Invoke(_currentLookPointCalculator);
        }

        private void OnCanUseSpellsAgain()
        {
            TryCastSelectedSpell();
        }

        private void TryCastSelectedSpell()
        {
            if (_isWaitingForAnimationFinish) return;
            _cachedSpell = _spellsSelector.GetSelectedSpellAndStartCooldownTimer();
            NeedChangeLookPointCalculator?.Invoke(_cachedSpell.LookPointCalculator);
            StateMachineControllable.StartPlayingActionAnimation(_cachedSpell.SpellAnimationData);
            _isWaitingForAnimationFinish = true;
        }

        private void CreateSelectedSpell(Quaternion direction)
        {
            _spellObjectsFactory.Create(_cachedSpell.SpellDataForSpellController,
                _cachedSpell.SpellPrefabProvider, _enemyController, _spellSpawnObject.Position, direction);

            _isWaitingForAnimationFinish = false;
            TryCastSelectedSpell();
        }
    }
}