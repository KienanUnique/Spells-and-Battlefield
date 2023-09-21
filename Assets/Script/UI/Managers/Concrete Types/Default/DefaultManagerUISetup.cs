using System.Collections.Generic;
using Common.Abstract_Bases;
using Systems.Input_Manager;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Loading_Window.Presenter;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Window.Presenter;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Managers.Concrete_Types.Default
{
    public class DefaultManagerUISetup : SetupMonoBehaviourBase
    {
        [SerializeField] private WindowPresenterBase _startWindow;
        [SerializeField] private LoadingWindowPresenter _loadingWindow;
        private IScenesController _scenesController;
        private IInputManagerForUI _inputManagerForUI;
        private IInitializableDefaultManagerUI _manager;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_startWindow, _loadingWindow};

        [Inject]
        private void GetDependencies(IScenesController scenesController, IInputManagerForUI inputManagerForUI)
        {
            _scenesController = scenesController;
            _inputManagerForUI = inputManagerForUI;
        }

        protected override void Prepare()
        {
            _manager = GetComponent<IInitializableDefaultManagerUI>();
        }

        protected override void Initialize()
        {
            var stackManager = new UIWindowsStackManager(_startWindow);
            _manager.Initialize(_inputManagerForUI, stackManager, _scenesController, _loadingWindow);
        }
    }
}