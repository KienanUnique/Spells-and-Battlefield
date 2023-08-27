﻿using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Element.View;

namespace UI.Element.Presenter
{
    public abstract class UIElementPresenterBase : InitializableMonoBehaviourBase, IUIElement
    {
        protected abstract IUIElementView View { get; }

        public virtual void Appear()
        {
            View.Appear();
        }

        public virtual void Disappear()
        {
            View.Disappear();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}