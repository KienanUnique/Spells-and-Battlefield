using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.View;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Presenter
{
    public class ComicsPanelPresenter : InitializableMonoBehaviourBase, IComicsPanel, IInitializableComicsPanelPresenter
    {
        private IComicsPanelView _view;

        public void Initialize(IComicsPanelView view)
        {
            _view = view;
            SetInitializedStatus();
            _view.DisappearWithoutAnimation();
        }

        public bool IsShown => _view.IsShown;

        public void Appear(PanelDelayType delayType, Action callbackOnComplete)
        {
            _view.Appear(delayType, callbackOnComplete);
        }

        public void Disappear()
        {
            _view.Disappear();
        }

        public void Disappear(Action callbackOnComplete)
        {
            _view.Disappear(callbackOnComplete);
        }

        public void SkipAnimation()
        {
            _view.SkipAnimation();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}