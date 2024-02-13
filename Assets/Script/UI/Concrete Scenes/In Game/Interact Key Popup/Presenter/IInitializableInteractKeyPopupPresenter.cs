using Player.Press_Key_Interactor;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Interact_Key_Popup.Presenter
{
    public interface IInitializableInteractKeyPopupPresenter
    {
        void Initialize(IUIElementView view, IPlayerAsPressKeyInteractor keyInteractor);
    }
}