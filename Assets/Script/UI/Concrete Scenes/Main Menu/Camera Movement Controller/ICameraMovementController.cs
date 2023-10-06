using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller
{
    public interface ICameraMovementController
    {
        public void MoveToNextPointOfView(ICollection<Vector3> waypoints, Action callBackOnFinish);
        public void ReturnToPreviousPointOfView();
    }
}