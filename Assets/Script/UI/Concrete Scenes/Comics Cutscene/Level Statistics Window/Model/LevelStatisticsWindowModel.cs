using Common.Id_Holder;
using Systems.Scenes_Controller;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Model
{
    public class LevelStatisticsWindowModel : UIWindowModelBase, ILevelStatisticsWindowModel
    {
        private readonly IInGameSceneController _sceneController;
        private readonly IUIWindowManager _windowManager;

        public LevelStatisticsWindowModel(IIdHolder idHolder, IInGameSceneController sceneController,
            IUIWindowManager windowManager) : base(idHolder)
        {
            _sceneController = sceneController;
            _windowManager = windowManager;
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnNextLevelButtonPressed()
        {
            _sceneController.LoadNextGameLevel();
        }

        public void OnRestartComicsButtonPressed()
        {
            _windowManager.TryCloseCurrentWindow();
        }
    }
}