using System;
using Common.Id_Holder;
using UI.Element.Presenter;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.View;

namespace UI.Window.Presenter
{
    public abstract class WindowPresenterBase : UIElementPresenterBase, IUIWindow
    {
        public int Id => WindowModel.Id;
        public bool CanBeClosedByPlayer => WindowModel.CanBeClosedByPlayer;
        protected abstract IUIWindowModel WindowModel { get; }
        protected abstract IUIWindowView WindowView { get; }
        protected sealed override IUIElementView View => WindowView;

        public bool Equals(IIdHolder other)
        {
            return WindowModel.Equals(other);
        }

        public override void Appear()
        {
            SubscribeOnWindowEvents();
            WindowModel.Appear();
            base.Appear();
        }

        public override void Disappear()
        {
            UnsubscribeFromWindowEvents();
            WindowModel.Disappear();
            base.Disappear();
        }

        public override void Disappear(Action callbackOnAnimationEnd)
        {
            UnsubscribeFromWindowEvents();
            WindowModel.Disappear();
            base.Disappear(callbackOnAnimationEnd);
        }

        protected abstract void SubscribeOnWindowEvents();
        protected abstract void UnsubscribeFromWindowEvents();

        protected sealed override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            if (WindowModel.IsOpened)
            {
                SubscribeOnWindowEvents();
            }
        }

        protected sealed override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            if (WindowModel.IsOpened)
            {
                UnsubscribeFromWindowEvents();
            }
        }
    }
}