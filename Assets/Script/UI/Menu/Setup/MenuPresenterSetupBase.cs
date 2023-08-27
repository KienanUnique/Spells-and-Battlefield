using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Settings.UI;
using UI.Element.View;
using UI.Managers.In_Game;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Menu.Setup
{
    public abstract class MenuPresenterSetupBase : WindowPresenterSetupBase
    {
        [SerializeField] protected Button _goToMainMenuButton;
        [SerializeField] protected Button _restartLevelButton;

        protected IInGameUIControllable _uiControllable;

        [Inject]
        private void Construct(IInGameUIControllable uiControllable)
        {
            _uiControllable = uiControllable;
        }

        protected IUIElementView View { get; private set; }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            View = new DefaultUIElementView(transform, _generalUIAnimationSettings);
        }
    }
}