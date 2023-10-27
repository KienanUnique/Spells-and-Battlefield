using System;

namespace UI.Element.View
{
    public interface IUIElementView
    {
        public void Appear();
        public void Disappear();
        public void Disappear(Action callbackOnAnimationEnd);
        public void DisappearWithoutAnimation();
    }
}