using UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Model;
using UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Setup;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Presenter
{
    public class LevelStatisticsWindowPresenter : WindowPresenterBase,
        IInitializableLevelStatisticWindowPresenter,
        ILevelStatisticsWindow
    {
        private ILevelStatisticsWindowModel _model;
        private IUIWindowView _view;
        private Button _nextLevelButton;
        private Button _restartComicsButton;

        public void Initialize(ILevelStatisticsWindowModel model, IUIWindowView view, Button nextLevelButton,
            Button restartComicsButton)
        {
            _model = model;
            _view = view;
            _nextLevelButton = nextLevelButton;
            _restartComicsButton = restartComicsButton;
            SetInitializedStatus();
        }

        protected override IUIWindowModel WindowModel => _model;
        protected override IUIWindowView WindowView => _view;

        protected override void SubscribeOnWindowEvents()
        {
            _nextLevelButton.onClick.AddListener(_model.OnNextLevelButtonPressed);
            _restartComicsButton.onClick.AddListener(_model.OnRestartComicsButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _nextLevelButton.onClick.RemoveListener(_model.OnNextLevelButtonPressed);
            _restartComicsButton.onClick.RemoveListener(_model.OnRestartComicsButtonPressed);
        }
    }
}