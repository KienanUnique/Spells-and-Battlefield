using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.In_Game.Aim_Icon.Presenter;
using UI.Element.Setup;
using UI.Element.View;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Aim_Icon.Setup
{
    public class UIAimIconPresenterSetup : UIElementPresenterSetup
    {
        [SerializeField] private Transform _thisTransform;
        private IInitializableUIAimIconPresenter _presenter;
        private IUIElementView _view;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableUIAimIconPresenter>();
            _view = new DefaultUIElementView(_thisTransform, DefaultUIElementViewSettings);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_view);
        }
    }
}