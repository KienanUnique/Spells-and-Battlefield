using System.Collections.Generic;
using System.Linq;
using Common;
using UI.Element.View;
using UI.Managers.In_Game;
using UI.Managers.In_Game.Installer;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Menu.Setup
{
    public abstract class MenuPresenterSetupBase : WindowPresenterSetupBase, IInGameUIControllableSettable
    {
        [SerializeField] protected Button _goToMainMenuButton;
        [SerializeField] protected Button _restartLevelButton;
        private IInGameUIControllable _uiControllable;
        private IUIElementView _view;
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;

        protected sealed override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(AdditionalObjectsToWaitBeforeInitialization)
                {_externalDependenciesInitializationWaiter};

        protected virtual IEnumerable<IInitializable> AdditionalObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        public void SetInGameUIControllable(IInGameUIControllable inGameUIControllable)
        {
            _uiControllable = inGameUIControllable;
            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        protected abstract void Initialize(IInGameUIControllable uiControllable, IUIElementView view);

        protected override void Prepare()
        {
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);
            _view = new DefaultUIElementView(transform, _generalUIAnimationSettings);
        }

        protected sealed override void Initialize()
        {
            Initialize(_uiControllable, _view);
        }
    }
}