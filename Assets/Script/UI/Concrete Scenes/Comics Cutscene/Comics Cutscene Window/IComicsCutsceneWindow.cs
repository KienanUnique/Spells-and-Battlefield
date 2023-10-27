using System;
using UI.Window;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window
{
    public interface IComicsCutsceneWindow : IUIWindow
    {
        public event Action ComicsFinished;
    }
}