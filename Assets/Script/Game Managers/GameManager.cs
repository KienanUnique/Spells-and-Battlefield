using System;
using System.Collections;
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
            HandleNewState(_currentGameState.Value);
        }

        private void OnEnable()
        {
            _currentGameState.AfterValueChanged += HandleNewState;
        }

        private void OnDisable()
        {
            _currentGameState.AfterValueChanged -= HandleNewState;
        }

        private void HandleNewState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Running:
                    _inGameManagerUI.SwitchToGameUI();
                    StartCoroutine(CheckPlayerState());
                    break;
                case GameState.GameOver:
                    _inGameManagerUI.SwitchToDeathMenuUI();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private IEnumerator CheckPlayerState()
        {
            while (_player.CurrentCharacterState != CharacterState.Dead)
            {
                yield return null;
            }

            _currentGameState.Value = GameState.GameOver;
        }

        private enum GameState
        {
            Running,
            GameOver
        }
    }
}