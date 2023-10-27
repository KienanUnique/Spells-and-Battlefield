using System.Collections.Generic;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Provider;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Data
{
    public interface IComicsData
    {
        public IReadOnlyList<IComicsScreenProvider> ScreensInOrder { get; }
    }
}