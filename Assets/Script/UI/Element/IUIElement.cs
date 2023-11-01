using System;

namespace UI.Element
{
    public interface IUIElement : IUISimpleElement
    {
        public void Disappear(Action callbackOnAnimationEnd);
    }
}