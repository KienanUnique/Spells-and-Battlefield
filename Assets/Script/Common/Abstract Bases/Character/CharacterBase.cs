using System;
using System.Collections.Generic;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Spells.Continuous_Effect;
using UnityEngine;

namespace Common.Abstract_Bases.Character
{
    public abstract class CharacterBase : BaseWithDisabling, ICharacterBase
    {
        protected readonly ValueWithReactionOnChange<float> _currentCountCountOfHitPoints;
        protected readonly ICoroutineStarter _coroutineStarter;
        private readonly List<IAppliedContinuousEffect> _currentEffects;
        private readonly CharacterSettingsSection _characterSettings;
        private readonly string _namePrefix;

        protected CharacterBase(ICoroutineStarter coroutineStarter, CharacterSettingsSection characterSettings,
            string namePrefix)
        {
            _coroutineStarter = coroutineStarter;
            _characterSettings = characterSettings;
            _namePrefix = namePrefix;
            CurrentState = new ValueWithReactionOnChange<CharacterState>(CharacterState.Alive);
            _currentCountCountOfHitPoints =
                new ValueWithReactionOnChange<float>(_characterSettings.MaximumCountOfHitPoints);
            _currentEffects = new List<IAppliedContinuousEffect>();
            SubscribeOnEvents();
        }

        public event Action<CharacterState> StateChanged;
        public event Action<float> HitPointsCountChanged;

        public float HitPointCountRatio =>
            _currentCountCountOfHitPoints.Value / _characterSettings.MaximumCountOfHitPoints;

        public ValueWithReactionOnChange<CharacterState> CurrentState { get; private set; }

        public void HandleHeal(int countOfHitPoints)
        {
            if (CurrentState.Value == CharacterState.Dead) return;
            _currentCountCountOfHitPoints.Value += countOfHitPoints;
            if (_currentCountCountOfHitPoints.Value > _characterSettings.MaximumCountOfHitPoints)
            {
                _currentCountCountOfHitPoints.Value = _characterSettings.MaximumCountOfHitPoints;
            }

            Debug.Log(
                $"{_namePrefix}: Handle_Heal<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints.Value}>, Current_State<{CurrentState.Value.ToString()}>");
        }

        public void HandleDamage(int countOfHitPoints)
        {
            if (CurrentState.Value == CharacterState.Dead) return;
            _currentCountCountOfHitPoints.Value -= countOfHitPoints;
            if (_currentCountCountOfHitPoints.Value <= 0)
            {
                CurrentState.Value = CharacterState.Dead;
                _currentCountCountOfHitPoints.Value = 0;
            }

            Debug.Log(
                $"{_namePrefix}: Handle_Damage<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints.Value}>, Current_State<{CurrentState.Value.ToString()}>");
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            if (CurrentState.Value == CharacterState.Dead) return;
            _currentEffects.Add(effect);
            effect.EffectEnded += OnEffectEnded;
            effect.Start(_coroutineStarter);
        }

        protected sealed override void SubscribeOnEvents()
        {
            CurrentState.AfterValueChanged += OnCharacterStateChanged;
            _currentCountCountOfHitPoints.AfterValueChanged += OnHitPointsCountChanged;
            _currentEffects.ForEach(effect => effect.EffectEnded += OnEffectEnded);
        }

        protected sealed override void UnsubscribeFromEvents()
        {
            CurrentState.AfterValueChanged -= OnCharacterStateChanged;
            _currentCountCountOfHitPoints.AfterValueChanged -= OnHitPointsCountChanged;
            _currentEffects.ForEach(effect => effect.EffectEnded -= OnEffectEnded);
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
                foreach (var effect in _currentEffects)
                {
                    effect.EffectEnded -= OnEffectEnded;
                    effect.End();
                }

                _currentEffects.Clear();
            }

            StateChanged?.Invoke(newState);
        }

        private void OnHitPointsCountChanged(float newHitPointsCount) =>
            HitPointsCountChanged?.Invoke(newHitPointsCount);
    }
}