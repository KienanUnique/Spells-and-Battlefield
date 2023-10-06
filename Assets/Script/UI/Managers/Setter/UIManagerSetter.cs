using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace UI.Managers.Setter
{
    public class UIManagerSetter : SetupMonoBehaviourBase
    {
        [SerializeField] private ManagerUIBase _managerUI;
        private IUIWindowManagerSettable[] _inGameUIControllableSettableObjects;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            foreach (IUIWindowManagerSettable dependentOnIInGameUIControllable in _inGameUIControllableSettableObjects)
            {
                dependentOnIInGameUIControllable.SetUIWindowManager(_managerUI);
            }
        }

        protected override void Prepare()
        {
            _inGameUIControllableSettableObjects = GetComponentsInChildren<IUIWindowManagerSettable>();
        }
    }
}