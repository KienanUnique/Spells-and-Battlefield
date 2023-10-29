using Systems.Scenes_Controller;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public abstract class InGameWindowPresenterSetupBase : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private Button _goToMainWindowButton;
        [SerializeField] private Button _restartLevelButton;

        [Inject]
        private void GetDependencies(IInGameSceneController inGameSceneController)
        {
            InGameSceneController = inGameSceneController;
        }

        protected Button GoToMainWindowButton => _goToMainWindowButton;
        protected Button RestartLevelButton => _restartLevelButton;
        protected IInGameSceneController InGameSceneController { private set; get; }
    }
}