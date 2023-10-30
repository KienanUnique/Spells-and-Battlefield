using UI.Window.Model;

namespace UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Model
{
    public interface ILevelStatisticsWindowModel : IUIWindowModel
    {
        public void OnNextLevelButtonPressed();
        public void OnRestartComicsButtonPressed();
    }
}