using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Id_Holder;
using UI.Element.Setup;
using UI.Managers.Concrete_Types.In_Game;
using UI.Managers.Installer;
using UnityEngine;

namespace UI.Window.Setup
{
    [RequireComponent(typeof(IdHolder))]
    public abstract class WindowPresenterSetupBase : UIElementPresenterSetup, IUIWindowManagerSettable
    {
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_externalDependenciesInitializationWaiter};

        protected IIdHolder IDHolder { get; private set; }
        protected IUIWindowManager Manager { get; private set; }

        public void SetUIWindowManager(IUIWindowManager manager)
        {
            Manager = manager;
            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        protected override void Prepare()
        {
            IDHolder = GetComponent<IIdHolder>();
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);
        }
    }
}