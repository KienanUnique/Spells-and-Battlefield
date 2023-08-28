using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace UI.Managers.In_Game.Installer
{
    public class InGameUIControllableSetter : SetupMonoBehaviourBase
    {
        [SerializeField] private InGameManagerUI _inGameManagerUI;
        private IInGameUIControllableSettable[] _inGameUIControllableSettableObjects;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _inGameUIControllableSettableObjects = GetComponentsInChildren<IInGameUIControllableSettable>();
        }

        protected override void Initialize()
        {
            foreach (var dependentOnIInGameUIControllable in _inGameUIControllableSettableObjects)
            {
                dependentOnIInGameUIControllable.SetInGameUIControllable(_inGameManagerUI);
            }
        }
    }
}