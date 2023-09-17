using Systems.Scene_Switcher;
using UI.Loading_Window.Presenter;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public abstract class InGameWindowPresenterSetupBase : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private LoadingWindowPresenter _loadingWindow;
        [SerializeField] private Button _goToMainWindowButton;
        [SerializeField] private Button _restartLevelButton;

        [Inject]
        private void GetDependencies(IInGameSceneManager inGameSceneManager)
        {
            InGameSceneManager = inGameSceneManager;
        }

        protected LoadingWindowPresenter LoadingWindow => _loadingWindow;
        protected Button GoToMainWindowButton => _goToMainWindowButton;
        protected Button RestartLevelButton => _restartLevelButton;
        protected IInGameSceneManager InGameSceneManager { private set; get; }
    }
}