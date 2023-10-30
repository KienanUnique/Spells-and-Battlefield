using UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window.Setup
{
    public interface IInitializableLevelStatisticWindowPresenter
    {
        void Initialize(ILevelStatisticsWindowModel model, IUIWindowView view, Button nextLevelButton,
            Button restartComicsButton);
    }
}