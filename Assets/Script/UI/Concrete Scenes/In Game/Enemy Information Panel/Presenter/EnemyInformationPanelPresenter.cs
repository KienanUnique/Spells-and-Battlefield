using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Setup;
using UI.Element.Presenter;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Presenter
{
    public class EnemyInformationPanelPresenter : UIElementPresenterBase,
        InitializableEnemyInformationPanelPresenter,
        IEnemyInformationPanelPresenter
    {
        private IUIElementView _view;

        public void Initialize(IUIElementView view)
        {
            _view = view;
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _view.Appear();
        }
    }
}