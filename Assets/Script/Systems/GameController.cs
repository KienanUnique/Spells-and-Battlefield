using System;
using Common;
using Common.Abstract_Bases.Character;
using Interfaces;
using Systems.Input_Manager;
using Systems.Time_Controller;
using UI;
using UnityEngine;
using Zenject;

namespace Systems
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
        private IPlayerInformationProvider _playerInformationProvider;
        private InGameInputManager _inGameMenuInput;
        private ITimeController _timeController;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider)
        {
            _playerInformationProvider = playerInformationProvider;
        }

        protected override void SpecialAwakeAction()
        {
            _currentGameState = new ValueWithReactionOnChange<GameState>(GameState.Playing);
            _lastState = _currentGameState.Value;
            _inGameMenuInput = GetComponent<InGameInputManager>();
            _timeController = GetComponent<TimeController>();
        }

        private void Start()
        {
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
            _playerInformationProvider.CharacterStateChanged += OnPlayerStateChanged;
            _inGameMenuInput.GamePause += OnOpenMenuInputted;
        }

        private void UnsubscribeFromPlayingEvents()
        {
            _playerInformationProvider.CharacterStateChanged -= OnPlayerStateChanged;
            _inGameMenuInput.GamePause -= OnOpenMenuInputted;
        }

        private void SubscribeOnPauseEvents()
        {
            _inGameMenuInput.GameContinue += OnCloseMenuInputted;
            _inGameManagerUI.GameContinueRequested += OnCloseMenuInputted;
            _inGameManagerUI.RestartRequested += OnRestartRequested;
        }

        private void UnsubscribeFromPauseEvents()
        {
            _inGameMenuInput.GameContinue -= OnCloseMenuInputted;
            _inGameManagerUI.GameContinueRequested -= OnCloseMenuInputted;
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
                    _inGameMenuInput.SwitchToGameInput();
                    _inGameManagerUI.SwitchToGameUI();
                    break;
                case GameState.GameOver:
                    SubscribeOnGameOverEvents();
                    _inGameMenuInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchToDeathMenuUI();
                    break;
                case GameState.Pause:
                    SubscribeOnPauseEvents();
                    _timeController.StopTime();
                    _inGameMenuInput.SwitchToUIInput();
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

        private void OnOpenMenuInputted()
        {
            _currentGameState.Value = GameState.Pause;
        }

        private void OnCloseMenuInputted()
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