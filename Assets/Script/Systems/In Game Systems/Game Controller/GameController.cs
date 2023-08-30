using System;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Interfaces;
using Systems.In_Game_Systems.Level_Finish_Zone;
using Systems.In_Game_Systems.Time_Controller;
using Systems.Input_Manager;
using Systems.Scene_Switcher;
using UI.Managers.In_Game;

namespace Systems.In_Game_Systems.Game_Controller
{
    public class GameController : InitializableMonoBehaviourBase, IInitializableGameController
    {
        private ValueWithReactionOnChange<GameState> _currentGameState;
        private GameState _lastState;

        private IInGameManagerUI _inGameManagerUI;
        private IPlayerInformationProvider _playerInformationProvider;
        private IInGameSystemInputManager _inGameSystemInput;
        private ITimeController _timeController;
        private ILevelFinishZone _levelFinishZone;
        private IInGameSceneSwitcher _inGameSceneSwitcher;

        public void Initialize(IInGameManagerUI inGameManagerUI, IPlayerInformationProvider playerInformationProvider,
            IInGameSystemInputManager inGameSystemInput, ITimeController timeController,
            ILevelFinishZone levelFinishZone, IInGameSceneSwitcher inGameSceneSwitcher)
        {
            _inGameManagerUI = inGameManagerUI;
            _playerInformationProvider = playerInformationProvider;
            _inGameSystemInput = inGameSystemInput;
            _timeController = timeController;
            _levelFinishZone = levelFinishZone;
            _inGameSceneSwitcher = inGameSceneSwitcher;
            _currentGameState = new ValueWithReactionOnChange<GameState>(GameState.Playing);
            _lastState = _currentGameState.Value;
            SetInitializedStatus();
            _timeController.RestoreTimeToNormal();
            OnAfterGameStateChanged(_currentGameState.Value);
        }

        protected override void SubscribeOnEvents()
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

        protected override void UnsubscribeFromEvents()
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
            _inGameSystemInput.GamePauseInputted += OnOpenMenuInputted;
        }

        private void UnsubscribeFromPlayingEvents()
        {
            _playerInformationProvider.CharacterStateChanged -= OnPlayerStateChanged;
            _levelFinishZone.PlayerEnterFinishZone -= OnPlayerEnterFinishZone;
            _inGameSystemInput.GamePauseInputted -= OnOpenMenuInputted;
        }

        private void SubscribeOnUIEvents()
        {
            _inGameSystemInput.CloseCurrentWindow += OnCloseCurrentWindow;
            _inGameManagerUI.AllMenusClosed += OnCloseMenuInputted;
            _inGameManagerUI.RestartLevelRequested += OnRestartRequested;
            _inGameManagerUI.LoadNextLevelRequested += OnLoadNextLevelRequested;
            _inGameManagerUI.QuitToMainMenuRequested += OnQuitToMainMenuRequested;
        }

        private void UnsubscribeFromUIEvents()
        {
            _inGameSystemInput.CloseCurrentWindow -= OnCloseCurrentWindow;
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
                    _inGameSystemInput.SwitchToGameInput();
                    break;
                case GameState.GameOver:
                    SubscribeOnUIEvents();
                    _inGameSystemInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.GameOverMenu);
                    break;
                case GameState.Pause:
                    SubscribeOnUIEvents();
                    _timeController.StopTime();
                    _inGameSystemInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.PauseMenu);
                    break;
                case GameState.LevelCompleted:
                    SubscribeOnUIEvents();
                    _inGameSystemInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.LevelCompletedMenu);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnQuitToMainMenuRequested()
        {
            _inGameSceneSwitcher.LoadMainMenu();
        }

        private void OnLoadNextLevelRequested()
        {
            _inGameSceneSwitcher.LoadNextLevel();
        }

        private void OnCloseCurrentWindow()
        {
            _inGameManagerUI.TryCloseCurrentWindow();
        }

        private void OnRestartRequested()
        {
            _inGameManagerUI.SwitchTo(InGameUIElementsGroup.LoadingWindow);
            _inGameSceneSwitcher.RestartLevel();
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