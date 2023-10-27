using System;
using UI.Window.View;

namespace UI.Concrete_Scenes.Main_Menu.View.Empty
{
    public class EmptyUIWindowView : IUIWindowView
    {
        public void Appear()
        {
        }

        public void Disappear()
        {
        }

        public void Disappear(Action callbackOnAnimationEnd)
        {
            callbackOnAnimationEnd?.Invoke();
        }

        public void DisappearWithoutAnimation()
        {
        }
    }
}