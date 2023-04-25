using System;
using Player;
using UI;
using UnityEngine;

namespace Game_Managers
{
    [RequireComponent(typeof(InteractableGameObjectsBankOfIds))]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private InGameManagerUI _inGameManagerUI;

        private ValueWithReactionOnChange<GameState> _currentGameState;

        private void Awake()
        {
            _currentGameState = new ValueWithReactionOnChange<GameState>(GameState.Running);
        }

        private void Start()
        {
            OnAfterGameStateChanged(_currentGameState.Value);
        }

        private void OnEnable()
        {
            _currentGameState.AfterValueChanged += OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged += OnBeforeGameStateChanged;
            if (_currentGameState.Value == GameState.Running)
            {
                _player.CurrentCharacterState.AfterValueChanged += OnPlayerStateChanged;
            }
        }

        private void OnDisable()
        {
            _currentGameState.AfterValueChanged -= OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged -= OnBeforeGameStateChanged;
            if (_currentGameState.Value == GameState.Running)
            {
                _player.CurrentCharacterState.AfterValueChanged -= OnPlayerStateChanged;
            }
        }

        private void OnBeforeGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Running:
                    _player.CurrentCharacterState.AfterValueChanged -= OnPlayerStateChanged;
                    break;
                case GameState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnAfterGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Running:
                    _inGameManagerUI.SwitchToGameUI();
                    _player.CurrentCharacterState.AfterValueChanged += OnPlayerStateChanged;
                    break;
                case GameState.GameOver:
                    _inGameManagerUI.SwitchToDeathMenuUI();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnPlayerStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _currentGameState.Value = GameState.GameOver;
            }
        }

        private enum GameState
        {
            Running,
            GameOver
        }
    }
}