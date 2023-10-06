using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ModestTree;
using UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller.Settings;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller
{
    public class CameraMovementController : ICameraMovementController
    {
        private readonly Transform _cameraTransform;
        private readonly ICameraMovementInMenuSceneSettings _cameraMovementSettings;
        private readonly Stack<CameraRoute> _passedRoutes = new Stack<CameraRoute>();

        private bool _needRotateCameraTowardsNextWaypoint;
        private Tweener _rotateCameraTowardsNextWaypointTweener;

        public CameraMovementController(Transform cameraTransform,
            ICameraMovementInMenuSceneSettings cameraMovementSettings)
        {
            _cameraTransform = cameraTransform;
            _cameraMovementSettings = cameraMovementSettings;
            _cameraTransform.DOKill();
        }

        public void MoveToNextPointOfView(ICollection<Vector3> waypoints, Action callBackOnFinish)
        {
            var routeToMove =
                new CameraRoute(_cameraTransform.position, waypoints, _cameraTransform.rotation.eulerAngles);
            Sequence forwardSequence = DOTween.Sequence();
            forwardSequence.ApplyCustomSetupForUI(_cameraTransform.gameObject);
            forwardSequence.Append(_cameraTransform
                                   .DOPath(routeToMove.ForwardRoute.ToArray(),
                                       _cameraMovementSettings.MoveAnimationDurationInSeconds,
                                       _cameraMovementSettings.MovePathType)
                                   .SetEase(_cameraMovementSettings.MoveEase)
                                   .OnWaypointChange(waypointIndex =>
                                   {
                                       if (waypointIndex >= routeToMove.ForwardRoute.Count)
                                       {
                                           return;
                                       }

                                       _cameraTransform
                                           .DOLookAt(routeToMove.ForwardRoute[waypointIndex],
                                               _cameraMovementSettings.MoveAnimationDurationInSeconds /
                                               routeToMove.ForwardRoute.Count)
                                           .SetEase(_cameraMovementSettings.RotateCameraEase)
                                           .ApplyCustomSetupForUI(_cameraTransform.gameObject);
                                   }))
                           .OnComplete(() =>
                           {
                               _passedRoutes.Push(routeToMove);
                               callBackOnFinish();
                           });
        }

        public void ReturnToPreviousPointOfView()
        {
            if (_passedRoutes.IsEmpty())
            {
                return;
            }

            CameraRoute routeToMove = _passedRoutes.Pop();
            Sequence returnSequence = DOTween.Sequence();
            returnSequence.ApplyCustomSetupForUI(_cameraTransform.gameObject);
            _needRotateCameraTowardsNextWaypoint = true;
            returnSequence.Append(_cameraTransform
                                  .DOPath(routeToMove.BackwardRoute.ToArray(),
                                      _cameraMovementSettings.MoveAnimationDurationInSeconds,
                                      _cameraMovementSettings.MovePathType)
                                  .SetEase(_cameraMovementSettings.MoveEase)
                                  .OnWaypointChange(waypointIndex =>
                                  {
                                      if (waypointIndex >= routeToMove.BackwardRoute.Count)
                                      {
                                          return;
                                      }

                                      if (_needRotateCameraTowardsNextWaypoint)
                                      {
                                          _rotateCameraTowardsNextWaypointTweener = _cameraTransform
                                              .DOLookAt(routeToMove.BackwardRoute[waypointIndex],
                                                  _cameraMovementSettings.MoveAnimationDurationInSeconds /
                                                  routeToMove.BackwardRoute.Count)
                                              .SetEase(_cameraMovementSettings.RotateCameraEase)
                                              .ApplyCustomSetupForUI(_cameraTransform.gameObject);
                                      }
                                  }))
                          .InsertCallback(
                              _cameraMovementSettings.MoveAnimationDurationInSeconds *
                              _cameraMovementSettings.PartOfTimelineToStartRotateCameraToOldRotation, () =>
                              {
                                  _needRotateCameraTowardsNextWaypoint = false;
                                  if (_rotateCameraTowardsNextWaypointTweener.IsActive())
                                  {
                                      _rotateCameraTowardsNextWaypointTweener.Kill();
                                  }

                                  _cameraTransform
                                      .DORotate(routeToMove.OriginalCameraRotation,
                                          _cameraMovementSettings.MoveAnimationDurationInSeconds *
                                          (1 - _cameraMovementSettings.PartOfTimelineToStartRotateCameraToOldRotation))
                                      .SetEase(_cameraMovementSettings.RotateCameraEase)
                                      .ApplyCustomSetupForUI(_cameraTransform.gameObject);
                              });
        }
    }
}