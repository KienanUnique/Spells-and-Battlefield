using System;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Player;
using Systems.Dialog;
using Systems.Dialog.Provider;
using Systems.In_Game_Systems.Level_Finish_Zone;
using Systems.In_Game_Systems.Time_Controller;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UI.Managers.Concrete_Types.In_Game;

namespace Systems.In_Game_Systems.Game_Controller
{
    public class GameController : InitializableMonoBehaviourBase, IInitializableGameController
    {
        private ValueWithReactionOnChange<GameState> _currentGameState;

        private IInGameManagerUI _inGameManagerUI;
        private IInGameSystemInputManager _inGameSystemInput;
        private GameState _lastState;
        private ILevelFinishZone _levelFinishZone;
        private IPlayerInformationProvider _playerInformationProvider;
        private ITimeController _timeController;
        private IDialogStarterForGameManager _dialogStarter;

        public void Initialize(IInGameManagerUI inGameManagerUI, IPlayerInformationProvider playerInformationProvider,
            IInGameSystemInputManager inGameSystemInput, ITimeController timeController,
            ILevelFinishZone levelFinishZone, IDialogStarterForGameManager dialogStarter)
        {
            _inGameManagerUI = inGameManagerUI;
            _playerInformationProvider = playerInformationProvider;
            _inGameSystemInput = inGameSystemInput;
            _timeController = timeController;
            _levelFinishZone = levelFinishZone;
            _currentGameState = new ValueWithReactionOnChange<GameState>(GameState.Playing);
            _lastState = _currentGameState.Value;
            _dialogStarter = dialogStarter;
            SetInitializedStatus();
            _timeController.RestoreTimeToNormal();
            OnAfterGameStateChanged(_currentGameState.Value);
        }

        private enum GameState
        {
            Playing,
            Pause,
            GameOver,
            LevelCompleted,
            InDialog
        }

        protected override void SubscribeOnEvents()
        {
            _currentGameState.AfterValueChanged += OnAfterGameStateChanged;
            _currentGameState.BeforeValueChanged += OnBeforeGameStateChanged;
            
            _dialogStarter.NeedStartDialog += OnNeedStartDialog;
            
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
            
            _dialogStarter.NeedStartDialog -= OnNeedStartDialog;

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
            _inGameManagerUI.AllWindowsClosed += OnCloseWindowInputted;
        }

        private void UnsubscribeFromUIEvents()
        {
            _inGameManagerUI.AllWindowsClosed -= OnCloseWindowInputted;
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
                case GameState.InDialog:
                    UnsubscribeFromUIEvents();
                    _timeController.RestoreTimeToPrevious();
                    _dialogStarter.HandleDialogEnd();
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
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.GameOverWindow);
                    break;
                case GameState.Pause:
                    SubscribeOnUIEvents();
                    _timeController.StopTime();
                    _inGameSystemInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.PauseWindow);
                    break;
                case GameState.LevelCompleted:
                    SubscribeOnUIEvents();
                    _timeController.StopTime();
                    _inGameSystemInput.SwitchToUIInput();
                    _inGameManagerUI.SwitchTo(InGameUIElementsGroup.LevelCompletedWindow);
                    break;
                case GameState.InDialog:
                    SubscribeOnUIEvents();
                    _inGameSystemInput.SwitchToUIInput();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnPlayerStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead && _currentGameState.Value == GameState.Playing)
            {
                _currentGameState.Value = GameState.GameOver;
            }
        }

        private void OnPlayerEnterFinishZone()
        {
            if (_currentGameState.Value == GameState.Playing)
            {
                _currentGameState.Value = GameState.LevelCompleted;
            }
        }

        private void OnOpenMenuInputted()
        {
            if (_currentGameState.Value == GameState.Playing)
            {
                _currentGameState.Value = GameState.Pause;
            }
        }

        private void OnCloseWindowInputted()
        {
            _currentGameState.Value = _lastState;
        }
        
        private void OnNeedStartDialog(IDialogProvider dialogProvider)
        {
            if (_currentGameState.Value == GameState.Playing)
            {
                _currentGameState.Value = GameState.InDialog;
                _inGameManagerUI.OpenDialog(dialogProvider);
            }
        }
    }
}