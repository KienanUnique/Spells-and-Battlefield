using UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller;
using UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Menus
{
    public class CameraMovementControllerInstaller : MonoInstaller
    {
        [SerializeField] private CameraMovementInMenuSceneSettings _cameraMovementInMenuSceneSettings;
        [SerializeField] private Camera _cameraToMove;

        public override void InstallBindings()
        {
            var cameraController =
                new CameraMovementController(_cameraToMove.transform, _cameraMovementInMenuSceneSettings);
            Container.Bind<ICameraMovementController>().FromInstance(cameraController).AsSingle();
        }
    }
}