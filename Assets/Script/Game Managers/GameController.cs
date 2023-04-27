using System;
using Interfaces;
using Player;
using UI;
using UnityEngine;

namespace Game_Managers
{
    [RequireComponent(typeof(InGameInputManager))]
    public class GameController : Singleton<GameController>
    {
        [SerializeField] private InGameManagerUI _inGameManagerUI;
        [SerializeField] private ScenesSwitcher _scenesSwitcher;

        private ValueWithReactionOnChange<GameState> _currentGameState;
        private bool _needSubscribeOnExternalDependenciesOnlyInStart = true;
        private IPlayer _player;

        public InGameInputManager InGameInputManager { get; private set; }

        public void OnRestartButtonPressed()
        {
            _inGameManagerUI.SwitchToLoadingScreen();
            _scenesSwitcher.LoadMainLevelScene();
        }

        protected override void SpecialAwakeAction()
        {
            _currentGameState = new ValueWithReactionOnChange<GameState>(GameState.Running);
            InGameInputManager = GetComponent<InGameInputManager>();
            _player = PlayerProvider.Instance.Player;
        }

        private void Start()
        {
            if (_currentGameState.Value == GameState.Running && _needSubscribeOnExternalDependenciesOnlyInStart)
            {
                _needSubscribeOnExternalDependenciesOnlyInStart = false;
                _player.CurrentCharacterState.AfterValueChanged += OnPlayerStateChanged;
            }

            OnAfterGameStateChanged(_currentGameState.Value);
        }

        private void OnEnable()
        {
            _currentGameState.AfterValueChanged += OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged += OnBeforeGameStateChanged;
            if (_currentGameState.Value == GameState.Running && !_needSubscribeOnExternalDependenciesOnlyInStart)
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
                    InGameInputManager.SwitchToGameInput();
                    _inGameManagerUI.SwitchToGameUI();
                    _player.CurrentCharacterState.AfterValueChanged += OnPlayerStateChanged;
                    break;
                case GameState.GameOver:
                    InGameInputManager.SwitchToUIInput();
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