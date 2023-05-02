using System;
using Game_Managers.Time_Controller;
using Interfaces;
using Player;
using UI;
using UnityEngine;

namespace Game_Managers
{
    [RequireComponent(typeof(InGameInputManager))]
    [RequireComponent(typeof(TimeController))]
    public class GameController : Singleton<GameController>
    {
        [SerializeField] private InGameManagerUI _inGameManagerUI;
        [SerializeField] private ScenesSwitcher _scenesSwitcher;

        private ValueWithReactionOnChange<GameState> _currentGameState;
        private GameState _lastState;
        private bool _needSubscribeOnEventsOnlyInStart = true;
        private IPlayer _player;
        private InGameInputManager _inGameInputManager;
        private ITimeController _timeController;

        protected override void SpecialAwakeAction()
        {
            _currentGameState = new ValueWithReactionOnChange<GameState>(GameState.Playing);
            _lastState = _currentGameState.Value;
            _inGameInputManager = GetComponent<InGameInputManager>();
            _timeController = GetComponent<TimeController>();
            _player = PlayerProvider.Instance.Player;
        }

        private void Start()
        {
            _player.Initialize(_inGameInputManager, _timeController);
            if (_needSubscribeOnEventsOnlyInStart)
            {
                _needSubscribeOnEventsOnlyInStart = false;
                SubscribeOnEvents();
            }
            
            _timeController.RestoreTimeToNormal();
            OnAfterGameStateChanged(_currentGameState.Value);
        }

        private void OnEnable()
        {
            if (!_needSubscribeOnEventsOnlyInStart)
            {
                SubscribeOnEvents();
            }
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            _currentGameState.AfterValueChanged += OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged += OnBeforeGameStateChanged;
            switch (_currentGameState.Value)
            {
                case GameState.Playing:
                    SubscribeOnPlayingEvents();
                    break;
                case GameState.Pause:
                    SubscribeOnPauseEvents();
                    break;
                case GameState.GameOver:
                    SubscribeOnGameOverEvents();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnsubscribeFromEvents()
        {
            _currentGameState.AfterValueChanged -= OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged -= OnBeforeGameStateChanged;
            switch (_currentGameState.Value)
            {
                case GameState.Playing:
                    UnsubscribeFromPlayingEvents();
                    break;
                case GameState.Pause:
                    UnsubscribeFromPauseEvents();
                    break;
                case GameState.GameOver:
                    UnsubscribeFromGameOverEvents();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SubscribeOnPlayingEvents()
        {
            _player.CurrentCharacterState.AfterValueChanged += OnPlayerStateChanged;
            _inGameInputManager.GamePause += OnGamePauseInputted;
        }

        private void UnsubscribeFromPlayingEvents()
        {
            _player.CurrentCharacterState.AfterValueChanged -= OnPlayerStateChanged;
            _inGameInputManager.GamePause -= OnGamePauseInputted;
        }

        private void SubscribeOnPauseEvents()
        {
            _inGameInputManager.GameContinue += OnGameContinueRequestedInputted;
            _inGameManagerUI.GameContinueRequested += OnGameContinueRequestedInputted;
            _inGameManagerUI.RestartRequested += OnRestartRequested;
        }

        private void UnsubscribeFromPauseEvents()
        {
            _inGameInputManager.GameContinue -= OnGameContinueRequestedInputted;
            _inGameManagerUI.GameContinueRequested -= OnGameContinueRequestedInputted;
            _inGameManagerUI.RestartRequested -= OnRestartRequested;
        }

        private void SubscribeOnGameOverEvents()
        {
            _inGameManagerUI.RestartRequested += OnRestartRequested;
        }

        private void UnsubscribeFromGameOverEvents()
        {
            _inGameManagerUI.RestartRequested -= OnRestartRequested;
        }

        private void OnBeforeGameStateChanged(GameState previousState)
        {
            switch (previousState)
            {
                case GameState.Playing:
                    UnsubscribeFromPlayingEvents();
                    break;
                case GameState.GameOver:
                    UnsubscribeFromGameOverEvents();
                    break;
                case GameState.Pause:
                    UnsubscribeFromPauseEvents();
                    _timeController.RestoreTimeToPrevious();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(previousState), previousState, null);
            }

            _lastState = previousState;
        }

        private void OnAfterGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Playing:
                    SubscribeOnPlayingEvents();
                    _inGameInputManager.SwitchToGameInput();
                    _inGameManagerUI.SwitchToGameUI();
                    break;
                case GameState.GameOver:
                    SubscribeOnGameOverEvents();
                    _inGameInputManager.SwitchToUIInput();
                    _inGameManagerUI.SwitchToDeathMenuUI();
                    break;
                case GameState.Pause:
                    SubscribeOnPauseEvents();
                    _timeController.StopTime();
                    _inGameInputManager.SwitchToUIInput();
                    _inGameManagerUI.SwitchToPauseScreen();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
        
        private void OnRestartRequested()
        {
            _inGameManagerUI.SwitchToLoadingScreen();
            _scenesSwitcher.LoadMainLevelScene();
        }

        private void OnPlayerStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _currentGameState.Value = GameState.GameOver;
            }
        }

        private void OnGamePauseInputted()
        {
            _currentGameState.Value = GameState.Pause;
        }

        private void OnGameContinueRequestedInputted()
        {
            _currentGameState.Value = _lastState;
        }

        private enum GameState
        {
            Playing,
            Pause,
            GameOver,
        }
    }
}