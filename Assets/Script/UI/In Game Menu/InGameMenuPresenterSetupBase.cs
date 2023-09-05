using Systems.Scene_Switcher;
using UI.Element.View;
using UI.Loading_Window.Presenter;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.In_Game_Menu
{
    public abstract class InGameMenuPresenterSetupBase : WindowPresenterSetupBase
    {
        [SerializeField] private LoadingWindowPresenter _loadingWindow;
        [SerializeField] private Button _goToMainMenuButton;
        [SerializeField] private Button _restartLevelButton;

        protected LoadingWindowPresenter LoadingWindow => _loadingWindow;
        protected Button GoToMainMenuButton => _goToMainMenuButton;
        protected Button RestartLevelButton => _restartLevelButton;
        protected IInGameSceneSwitcher InGameSceneSwitcher { private set; get; }
        protected IUIElementView View { private set; get; }

        [Inject]
        private void Construct(IInGameSceneSwitcher inGameSceneSwitcher)
        {
            InGameSceneSwitcher = inGameSceneSwitcher;
        }

        protected override void Prepare()
        {
            base.Prepare();
            View = new DefaultUIElementView(transform, DefaultUIElementViewSettings);
        }
    }
}