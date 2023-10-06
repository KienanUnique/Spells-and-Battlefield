using System.Collections.Generic;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller
{
    public class CameraRoute
    {
        private readonly List<Vector3> _forwardRoute;
        private readonly List<Vector3> _backwardRoute;

        public CameraRoute(Vector3 originalCameraPosition, ICollection<Vector3> waypoints,
            Vector3 originalCameraRotation)
        {
            _forwardRoute = new List<Vector3>(waypoints);
            _backwardRoute = new List<Vector3>(waypoints);
            _backwardRoute.Reverse();
            _backwardRoute.Add(originalCameraPosition);
            OriginalCameraRotation = originalCameraRotation;
        }

        public IReadOnlyList<Vector3> ForwardRoute => _forwardRoute;
        public IReadOnlyList<Vector3> BackwardRoute => _backwardRoute;
        public Vector3 OriginalCameraRotation { get; }
    }
}