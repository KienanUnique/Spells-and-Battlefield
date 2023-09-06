using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Element.Setup;
using UI.Element.View;
using UI.Player_Information_Panel.Presenter;

namespace UI.Player_Information_Panel.Setup
{
    public class PlayerInformationPanelPresenterSetup : UIElementPresenterSetup
    {
        private IInitializablePlayerInformationPanelPresenter _presenter;
        private IUIElementView _view;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _presenter.Initialize(_view);
        }

        protected override void Prepare()
        {
            _view = new DefaultUIElementView(transform, DefaultUIElementViewSettings);
            _presenter = GetComponent<IInitializablePlayerInformationPanelPresenter>();
        }
    }
}