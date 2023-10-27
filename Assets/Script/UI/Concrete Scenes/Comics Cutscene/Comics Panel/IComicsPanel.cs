using System;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel
{
    public interface IComicsPanel
    {
        public bool IsShown { get; }
        public void Appear(PanelDelayType delayType, Action callbackOnComplete);
        public void Disappear();
        public void Disappear(Action callbackOnComplete);
        public void SkipAnimation();
    }
}