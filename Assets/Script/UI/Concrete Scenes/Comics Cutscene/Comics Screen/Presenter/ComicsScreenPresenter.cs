using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Model;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Setup;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Presenter
{
    public class ComicsScreenPresenter : InitializableMonoBehaviourBase,
        IInitializableComicsScreen,
        IInitializableComicsScreenPresenter
    {
        private IComicsScreenModel _model;

        public void Initialize(IComicsScreenModel model)
        {
            _model = model;
            SetInitializedStatus();
        }

        public event Action AllPanelsShown;

        public void SkipPanelAnimation()
        {
            _model.SkipPanelAnimation();
        }

        public void Disappear(Action callbackOnAnimationEnd)
        {
            _model.Disappear(callbackOnAnimationEnd);
        }

        public void Appear()
        {
            _model.Appear();
        }

        public void Disappear()
        {
            _model.Disappear();
        }

        protected override void SubscribeOnEvents()
        {
            _model.AllPanelsShown += OnAllPanelsShown;
        }

        protected override void UnsubscribeFromEvents()
        {
            _model.AllPanelsShown -= OnAllPanelsShown;
        }

        private void OnAllPanelsShown()
        {
            AllPanelsShown?.Invoke();
        }
    }
}