using System;
using System.Collections.Generic;
using General_Settings_in_Scriptable_Objects;
using Spells;
using Spells.Continuous_Effect;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected ValueWithReactionOnChange<float> _currentCountCountOfHitPoints;
    protected List<IContinuousEffect> _currentEffects;
    public event Action<CharacterState> StateChanged;
    public event Action<float> HitPointsCountChanged;
    public float HitPointCountRatio => _currentCountCountOfHitPoints.Value / CharacterSettings.MaximumCountOfHitPoints;
    public ValueWithReactionOnChange<CharacterState> CurrentState { get; private set; }
    protected abstract string NamePrefix { get; }
    protected abstract CharacterSettingsSection CharacterSettings { get; }

    public void HandleHeal(int countOfHitPoints)
    {
        if (CurrentState.Value == CharacterState.Dead) return;
        _currentCountCountOfHitPoints.Value += countOfHitPoints;
        if (_currentCountCountOfHitPoints.Value > CharacterSettings.MaximumCountOfHitPoints)
        {
            _currentCountCountOfHitPoints.Value = CharacterSettings.MaximumCountOfHitPoints;
        }

        Debug.Log(
            $"{NamePrefix}: Handle_Heal<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints.Value}>, Current_State<{CurrentState.Value.ToString()}>");
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
            $"{NamePrefix}: Handle_Damage<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints.Value}>, Current_State<{CurrentState.Value.ToString()}>");
    }

    public void ApplyContinuousEffect(IContinuousEffect effect)
    {
        if (CurrentState.Value == CharacterState.Dead) return;
        _currentEffects.Add(effect);
        effect.EffectEnded += OnEffectEnded;
        effect.Start(this);
    }

    private void Awake()
    {
        CurrentState = new ValueWithReactionOnChange<CharacterState>(CharacterState.Alive);
        _currentCountCountOfHitPoints = new ValueWithReactionOnChange<float>(CharacterSettings.MaximumCountOfHitPoints);
        _currentEffects = new List<IContinuousEffect>();
    }

    private void OnEnable()
    {
        CurrentState.AfterValueChanged += OnCharacterStateChanged;
        _currentCountCountOfHitPoints.AfterValueChanged += OnHitPointsCountChanged;
        _currentEffects.ForEach(effect => effect.EffectEnded += OnEffectEnded);
    }

    private void OnDisable()
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

    private void OnHitPointsCountChanged(float newHitPointsCount) => HitPointsCountChanged?.Invoke(newHitPointsCount);
}