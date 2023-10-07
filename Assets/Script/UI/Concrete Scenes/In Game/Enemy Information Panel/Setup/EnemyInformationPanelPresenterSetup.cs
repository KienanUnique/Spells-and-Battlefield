using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Element.Setup;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Setup
{
    public class EnemyInformationPanelPresenterSetup : UIElementPresenterSetup
    {
        private IUIElementView _view;
        private InitializableEnemyInformationPanelPresenter _presenter;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _presenter = GetComponent<InitializableEnemyInformationPanelPresenter>();
            _view = new DefaultUIElementView(transform, DefaultUIElementViewSettings);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_view);
        }
    }
}