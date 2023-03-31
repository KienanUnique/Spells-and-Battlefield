using System;
using System.Collections.Generic;
using Spells;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Action<CharacterState> CharacterStateChanged;
    [SerializeField] protected float _maximumCountOfHitPoints;
    protected float _currentCountCountOfHitPoints;
    protected List<IContinuousEffect> _currentEffects;
    protected ValueWithReactionOnChange<CharacterState> CurrentState { get; private set; }
    protected abstract string NamePrefix { get; }

    public void HandleHeal(int countOfHitPoints)
    {
        if (CurrentState.Value == CharacterState.Dead) return;
        _currentCountCountOfHitPoints += countOfHitPoints;
        Debug.Log(
            $"{NamePrefix}: Handle_Heal<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints}>, Current_State<{CurrentState.Value.ToString()}>");
    }

    public void HandleDamage(int countOfHitPoints)
    {
        if (CurrentState.Value == CharacterState.Dead) return;
        _currentCountCountOfHitPoints -= countOfHitPoints;
        if (_currentCountCountOfHitPoints <= 0)
        {
            CurrentState.Value = CharacterState.Dead;
            _currentCountCountOfHitPoints = 0;
        }

        Debug.Log(
            $"{NamePrefix}: Handle_Damage<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints}>, Current_State<{CurrentState.Value.ToString()}>");
    }

    public void ApplyContinuousEffect(IContinuousEffect effect)
    {
        if (CurrentState.Value == CharacterState.Dead) return;
        _currentEffects.Add(effect);
        effect.EffectEnded += OnEffectEnded;
        effect.Start(this);
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

        CharacterStateChanged?.Invoke(newState);
    }

    private void Awake()
    {
        _currentCountCountOfHitPoints = _maximumCountOfHitPoints;
        CurrentState = new ValueWithReactionOnChange<CharacterState>(CharacterState.Alive);
        _currentEffects = new List<IContinuousEffect>();
    }

    private void OnEnable()
    {
        CurrentState.ValueChanged += OnCharacterStateChanged;
        _currentEffects.ForEach(effect => effect.EffectEnded += OnEffectEnded);
    }

    private void OnDisable()
    {
        CurrentState.ValueChanged -= OnCharacterStateChanged;
        _currentEffects.ForEach(effect => effect.EffectEnded -= OnEffectEnded);
    }
}