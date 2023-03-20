using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Action<CharacterState> CharacterStateChanged;
    [SerializeField] protected float _maximumCountOfHitPoints;
    protected float _currentCountCountOfHitPoints;
    protected ValueWithReactionOnChange<CharacterState> CurrentState { get; private set; }
    protected abstract string NamePrefix { get; }

    public void HandleHeal(int countOfHitPoints)
    {
        _currentCountCountOfHitPoints += countOfHitPoints;
        Debug.Log(
            $"{NamePrefix}: Handle_Heal<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints}>, Current_State<{CurrentState.Value.ToString()}>");
    }

    public void HandleDamage(int countOfHitPoints)
    {
        if (CurrentState.Value == CharacterState.Alive)
        {
            _currentCountCountOfHitPoints -= countOfHitPoints;
            if (_currentCountCountOfHitPoints <= 0)
            {
                CurrentState.Value = CharacterState.Dead;
                _currentCountCountOfHitPoints = 0;
            }
        }

        Debug.Log(
            $"{NamePrefix}: Handle_Damage<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints}>, Current_State<{CurrentState.Value.ToString()}>");
    }

    private void Awake()
    {
        _currentCountCountOfHitPoints = _maximumCountOfHitPoints;
        CurrentState = new ValueWithReactionOnChange<CharacterState>(CharacterState.Alive);
    }

    private void OnEnable()
    {
        CurrentState.ValueChanged += (newState) => CharacterStateChanged?.Invoke(newState);
    }

    private void OnDisable()
    {
        CurrentState.ValueChanged -= (newState) => CharacterStateChanged?.Invoke(newState);
    }
}