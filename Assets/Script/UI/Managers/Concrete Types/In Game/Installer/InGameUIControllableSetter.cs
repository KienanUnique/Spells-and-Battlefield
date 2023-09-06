using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace UI.Managers.Concrete_Types.In_Game.Installer
{
    public class InGameUIControllableSetter : SetupMonoBehaviourBase
    {
        [SerializeField] private InGameManagerUI _inGameManagerUI;
        private IUIWindowManagerSettable[] _inGameUIControllableSettableObjects;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            foreach (IUIWindowManagerSettable dependentOnIInGameUIControllable in _inGameUIControllableSettableObjects)
            {
                dependentOnIInGameUIControllable.SetInGameUIControllable(_inGameManagerUI);
            }
        }

        protected override void Prepare()
        {
            _inGameUIControllableSettableObjects = GetComponentsInChildren<IUIWindowManagerSettable>();
        }
    }
}