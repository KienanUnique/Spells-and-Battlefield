using Player.Press_Key_Interactor;
using UI.Element.Presenter;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Interact_Key_Popup.Presenter
{
    public class InteractKeyPopupPresenter : UIElementPresenterBase, IInitializableInteractKeyPopupPresenter
    {
        private IUIElementView _view;
        private IPlayerAsPressKeyInteractor _keyInteractor;

        public void Initialize(IUIElementView view, IPlayerAsPressKeyInteractor keyInteractor)
        {
            _view = view;
            _keyInteractor = keyInteractor;

            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;

        public override void Appear()
        {
            if (_keyInteractor.CanInteract)
            {
                base.Appear();
            }
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _keyInteractor.CanInteractNow += Appear;
            _keyInteractor.CanNotInteractNow += Disappear;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _keyInteractor.CanInteractNow -= Appear;
            _keyInteractor.CanNotInteractNow -= Disappear;
        }
    }
}