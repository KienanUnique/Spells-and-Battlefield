using System.Collections.Generic;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Presenter;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Model;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Presenter;
using UI.Concrete_Scenes.Main_Menu.View.With_Camera_Movement;
using UI.Window.Setup;
using UI.Window.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Setup
{
    public class StartGameWindowPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private GameLevelSelectorPresenter _gameLevelSelector;
        [SerializeField] private List<Transform> _waypointsTransforms;
        private IUIWindowView _view;
        private ICameraMovementController _cameraMovementController;
        private IScenesController _scenesController;
        private IInitializableStartGameWindowPresenter _presenter;

        [Inject]
        private void GetDependencies(IScenesController scenesController,
            ICameraMovementController cameraMovementController)
        {
            _scenesController = scenesController;
            _cameraMovementController = cameraMovementController;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(base.ObjectsToWaitBeforeInitialization) {_gameLevelSelector};

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableStartGameWindowPresenter>();
            var waypoints = new List<Vector3>();
            _waypointsTransforms.ForEach(waypointTransform => waypoints.Add(waypointTransform.position));
            _view = new UIWindowViewWithCameraMovement(transform, DefaultUIElementViewSettings, waypoints,
                _cameraMovementController);
        }

        protected override void Initialize()
        {
            var model = new StartGameWindowModel(IDHolder, Manager, _gameLevelSelector, _scenesController);
            _presenter.Initialize(model, _view, _backButton, _loadButton);
        }
    }
}