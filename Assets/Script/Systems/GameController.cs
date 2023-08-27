using System;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Character;
using Interfaces;
using Systems.Input_Manager;
using Systems.Level_Finish_Zone;
using Systems.Scene_Switcher.Concrete_Types;
using Systems.Time_Controller;
using UI.Managers.In_Game;
using UnityEngine;
using Zenject;

namespace Systems
{
    [RequireComponent(typeof(InGameInputManager))]
    [RequireComponent(typeof(TimeController))]
    [RequireComponent(typeof(SceneStarter))]
    public class GameController : Singleton<GameController>
    {
        [SerializeField] private InGameManagerUI _inGameManagerUI;
        [SerializeField] private InGameScenesSwitcher _inGameScenesSwitcher;

        private ValueWithReactionOnChange<GameState> _currentGameState;
        private GameState _lastState;
        private bool _needSubscribeOnEventsOnlyInStart = true;
        private IPlayerInformationProvider _playerInformationProvider;
        private InGameInputManager _inGameMenuInput;
        private ITimeController _timeController;
        private ILevelFinishZone _levelFinishZone;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider, ILevelFinishZone levelFinishZone)
        {
            _playerInformationProvider = playerInformationProvider;
            _levelFinishZone = levelFinishZone;
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
            if (_currentGameState.Value == GameState.Playing)
            {
                SubscribeOnPlayingEvents();
            }
            else
            {
                SubscribeOnUIEvents();
            }
        }

        private void UnsubscribeFromEvents()
        {
            _currentGameState.AfterValueChanged -= OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged -= OnBeforeGameStateChanged;

            if (_currentGameState.Value == GameState.Playing)
            {
                UnsubscribeFromPlayingEvents();
            }
            else
            {
                UnsubscribeFromUIEvents();
            }
        }

        private void SubscribeOnPlayingEvents()
        {
            _playerInformationProvider.CharacterStateChanged += OnPlayerStateChanged;
            _levelFinishZone.PlayerEnterFinishZone += OnPlayerEnterFinishZone;
            _inGameMenuInput.GamePause += OnOpenMenuInputted;
        }

        private void UnsubscribeFromPlayingEvents()
        {
            _playerInformationProvider.CharacterStateChanged -= OnPlayerStateChanged;
            _levelFinishZone.PlayerEnterFinishZone -= OnPlayerEnterFinishZone;
            _inGameMenuInput.GamePause -= OnOpenMenuInputted;
        }

        private void SubscribeOnUIEvents()
        {
            _inGameMenuInput.CloseCurrentWindow += OnCloseCurrentWindow;
            _inGameManagerUI.AllMenusClosed += OnCloseMenuInputted;
            _inGameManagerUI.RestartLevelRequested += OnRestartRequested;
            _inGameManagerUI.LoadNextLevelRequested += OnLoadNextLevelRequested;
            _inGameManagerUI.QuitToMainMenuRequested += OnQuitToMainMenuRequested;
        }

        private void UnsubscribeFromUIEvents()
        {
            _inGameMenuInput.CloseCurrentWindow -= OnCloseCurrentWindow;
            _inGameManagerUI.AllMenusClosed -= OnCloseMenuInputted;
            _inGameManagerUI.RestartLevelRequested -= OnRestartRequested;
            _inGameManagerUI.LoadNextLevelRequested -= OnLoadNextLevelRequested;
            _inGameManagerUI.QuitToMainMenuRequested -= OnQuitToMainMenuRequested;
        }

        private void OnBeforeGameStateChanged(GameState previousState)
        {
            switch (previousState)
            {
                case GameState.Playing:
                    UnsubscribeFromPlayingEvents();
                    break;
                case GameState.GameOver:
                    UnsubscribeFromUIEvents();
                    break;
                case GameState.Pause:
                    UnsubscribeFromUIEvents();
                    _timeController.RestoreTimeToPrevious();
                    break;
                case GameState.LevelCompleted:
                    UnsubscribeFromUIEvents();
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
                    break;
                case GameState.GameOver:
                    SubscribeOnUIEvents();
                    _inGameMenuInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.GameOverMenu);
                    break;
                case GameState.Pause:
                    SubscribeOnUIEvents();
                    _timeController.StopTime();
                    _inGameMenuInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.PauseMenu);
                    break;
                case GameState.LevelCompleted:
                    SubscribeOnUIEvents();
                    _inGameMenuInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.LevelCompletedMenu);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnQuitToMainMenuRequested()
        {
            _inGameScenesSwitcher.LoadMainMenu();
        }

        private void OnLoadNextLevelRequested()
        {
            _inGameScenesSwitcher.LoadNextLevel();
        }

        private void OnCloseCurrentWindow()
        {
            _inGameManagerUI.TryCloseCurrentWindow();
        }

        private void OnRestartRequested()
        {
            _inGameManagerUI.SwitchTo(InGameUIElementsGroup.LoadingWindow);
            _inGameScenesSwitcher.RestartLevel();
        }

        private void OnPlayerStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _currentGameState.Value = GameState.GameOver;
            }
        }

        private void OnPlayerEnterFinishZone()
        {
            _currentGameState.Value = GameState.LevelCompleted;
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
            LevelCompleted
        }
    }
}