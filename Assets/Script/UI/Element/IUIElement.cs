using System;

namespace UI.Element
{
    public interface IUIElement : IUIElementModel
    {
        public void Disappear(Action callbackOnAnimationEnd);
    }
}