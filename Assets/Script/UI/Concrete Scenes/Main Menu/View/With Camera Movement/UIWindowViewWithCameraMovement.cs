using System.Collections.Generic;
using UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller;
using UI.Element.View.Settings;
using UI.Window.View;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.View.With_Camera_Movement
{
    public class UIWindowViewWithCameraMovement : DefaultUIWindowView
    {
        private readonly ICollection<Vector3> _cameraWaypoints;
        private readonly ICameraMovementController _cameraMovementController;

        public UIWindowViewWithCameraMovement(Transform cachedTransform, IDefaultUIElementViewSettings settings,
            ICollection<Vector3> cameraWaypoints, ICameraMovementController cameraMovementController) : base(
            cachedTransform, settings)
        {
            _cameraWaypoints = cameraWaypoints;
            _cameraMovementController = cameraMovementController;
        }

        public override void Appear()
        {
            _cameraMovementController.MoveToNextPointOfView(_cameraWaypoints, base.Appear);
        }

        public override void Disappear()
        {
            base.Disappear();
            _cameraMovementController.ReturnToPreviousPointOfView();
        }
    }
}