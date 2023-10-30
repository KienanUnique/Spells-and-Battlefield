using Systems.Scenes_Controller;
using UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Model;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Setup
{
    public class LevelStatisticsWindowPresenterSetup : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartComicsButton;
        private IInGameSceneController _sceneController;
        private IInitializableLevelStatisticWindowPresenter _presenter;
        private ILevelStatisticsWindowModel _model;

        [Inject]
        private void GetDependencies(IInGameSceneController sceneController)
        {
            _sceneController = sceneController;
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableLevelStatisticWindowPresenter>();
        }

        protected override void Initialize()
        {
            _model = new LevelStatisticsWindowModel(IDHolder, _sceneController, Manager);
            _presenter.Initialize(_model, View, _nextLevelButton, _restartComicsButton);
        }
    }
}