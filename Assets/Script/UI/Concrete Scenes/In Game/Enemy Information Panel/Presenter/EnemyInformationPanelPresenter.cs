using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Setup;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Presenter
{
    public class EnemyInformationPanelPresenter : InitializableMonoBehaviourBase,
        InitializableEnemyInformationPanelPresenter,
        IEnemyInformationPanelPresenter
    {
        private IUIElementView _view;

        public void Initialize(IUIElementView view)
        {
            _view = view;
            SetInitializedStatus();
        }

        public void Appear()
        {
            _view.Appear();
        }

        public void Disappear()
        {
            _view.Disappear();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}