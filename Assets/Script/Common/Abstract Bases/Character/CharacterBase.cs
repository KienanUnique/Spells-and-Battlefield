using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Settings.Sections.Character;
using Interfaces;

namespace Common.Abstract_Bases.Character
{
    public abstract class CharacterBase : BaseWithDisabling, ICharacterBase
    {
        protected readonly ICharacterSettings _characterSettings;
        protected readonly ICoroutineStarter _coroutineStarter;
        protected readonly List<IAppliedContinuousEffect> _currentEffects;
        protected readonly ValueWithReactionOnChange<CharacterState> _currentState;
        protected readonly HitPointsCalculator _hitPointsCalculator;

        protected CharacterBase(ICoroutineStarter coroutineStarter, ICharacterSettings characterSettings)
        {
            _coroutineStarter = coroutineStarter;
            _characterSettings = characterSettings;
            _currentState = new ValueWithReactionOnChange<CharacterState>(CharacterState.Alive);
            _hitPointsCalculator = new HitPointsCalculator(_characterSettings.MaximumCountOfHitPoints);
            _currentEffects = new List<IAppliedContinuousEffect>();
        }

        public event Action<CharacterState> CharacterStateChanged;
        public event ICharacterInformationProvider.OnHitPointsCountChanged HitPointsCountChanged;

        public CharacterState CurrentCharacterState => _currentState.Value;
        public float HitPointCountRatio => _hitPointsCalculator.HitPointCountRatio;

        public virtual void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            if (_currentState.Value == CharacterState.Dead)
            {
                return;
            }

            _currentEffects.Add(effect);
            effect.EffectEnded += OnEffectEnded;
            effect.Start(_coroutineStarter);
        }

        public virtual void HandleDamage(int countOfHitPoints)
        {
            if (_currentState.Value == CharacterState.Dead)
            {
                return;
            }

            _hitPointsCalculator.HandleDamage(countOfHitPoints);
        }

        public virtual void HandleHeal(int countOfHitPoints)
        {
            if (_currentState.Value == CharacterState.Dead)
            {
                return;
            }

            _hitPointsCalculator.HandleHeal(countOfHitPoints);
        }

        protected sealed override void SubscribeOnEvents()
        {
            _currentState.AfterValueChanged += OnCharacterStateChanged;
            _hitPointsCalculator.HitPointsCountChanged += OnHitPointsCountChanged;
            _currentEffects.ForEach(effect => effect.EffectEnded += OnEffectEnded);
        }

        protected sealed override void UnsubscribeFromEvents()
        {
            _currentState.AfterValueChanged -= OnCharacterStateChanged;
            _hitPointsCalculator.HitPointsCountChanged -= OnHitPointsCountChanged;
            _currentEffects.ForEach(effect => effect.EffectEnded -= OnEffectEnded);
        }

        private void OnHitPointsCountChanged(int hitPointsLeft, int hitPointsChangeValue,
            TypeOfHitPointsChange typeOfHitPointsChange)
        {
            if (_hitPointsCalculator.CurrentCountOfHitPoints == 0)
            {
                _currentState.Value = CharacterState.Dead;
            }

            HitPointsCountChanged?.Invoke(hitPointsLeft, hitPointsChangeValue, typeOfHitPointsChange);
        }

        private void OnEffectEnded(IContinuousEffect obj)
        {
            obj.EffectEnded -= OnEffectEnded;
            _currentEffects.Remove(obj);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                foreach (IAppliedContinuousEffect effect in _currentEffects)
                {
                    effect.EffectEnded -= OnEffectEnded;
                    effect.End();
                }

                _currentEffects.Clear();
            }

            CharacterStateChanged?.Invoke(newState);
        }

        protected class HitPointsCalculator
        {
            private readonly int _maximumCountOfHitPoints;

            public HitPointsCalculator(int maximumCountOfHitPoints)
            {
                _maximumCountOfHitPoints = maximumCountOfHitPoints;
                CurrentCountOfHitPoints = _maximumCountOfHitPoints;
            }

            public event ICharacterInformationProvider.OnHitPointsCountChanged HitPointsCountChanged;

            public float HitPointCountRatio => 1.0f * CurrentCountOfHitPoints / _maximumCountOfHitPoints;
            public int CurrentCountOfHitPoints { get; private set; }

            public void HandleDamage(int countOfHitPoints)
            {
                if (countOfHitPoints <= 0)
                {
                    return;
                }

                int oldCountOfHitPoints = CurrentCountOfHitPoints;
                CurrentCountOfHitPoints -= countOfHitPoints;
                if (CurrentCountOfHitPoints < 0)
                {
                    CurrentCountOfHitPoints = 0;
                }

                HitPointsCountChanged?.Invoke(CurrentCountOfHitPoints, oldCountOfHitPoints - CurrentCountOfHitPoints,
                    TypeOfHitPointsChange.Damage);
            }

            public void HandleHeal(int countOfHitPoints)
            {
                if (countOfHitPoints >= _maximumCountOfHitPoints)
                {
                    return;
                }

                int oldCountOfHitPoints = CurrentCountOfHitPoints;
                CurrentCountOfHitPoints += countOfHitPoints;
                if (CurrentCountOfHitPoints > _maximumCountOfHitPoints)
                {
                    CurrentCountOfHitPoints = _maximumCountOfHitPoints;
                }

                HitPointsCountChanged?.Invoke(CurrentCountOfHitPoints, CurrentCountOfHitPoints - oldCountOfHitPoints,
                    TypeOfHitPointsChange.Heal);
            }
        }
    }
}