using System;
using UI.Element;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Model
{
    public interface IComicsScreenModel : IUIElement
    {
        public event Action AllPanelsShown;
        public void SkipPanelAnimation();
    }
}