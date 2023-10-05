using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UI.Concrete_Scenes.Main_Menu.View.With_Camera_Movement.Settings;
using UI.Element.View.Settings;
using UI.Window.View;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.View.With_Camera_Movement
{
    public class UIWindowViewWithCameraMovement : DefaultUIWindowView
    {
        private readonly List<Vector3> _cameraWaypoints;
        private readonly Transform _cameraTransform;
        private readonly ICameraMovementInMenuSceneSettings _cameraMovementSettings;
        private Vector3 _cameraOldRotation;
        private Vector3 _cameraOldPosition;

        public UIWindowViewWithCameraMovement(Transform cachedTransform, IDefaultUIElementViewSettings settings,
            IEnumerable<Vector3> cameraWaypoints, Transform cameraTransform,
            ICameraMovementInMenuSceneSettings cameraMovementSettings) : base(cachedTransform, settings)
        {
            _cameraWaypoints = new List<Vector3>(cameraWaypoints);
            _cameraTransform = cameraTransform;
            _cameraMovementSettings = cameraMovementSettings;
        }

        public override void Appear()
        {
            _cameraOldRotation = _cameraTransform.rotation.eulerAngles;
            _cameraOldPosition = _cameraTransform.position;
            Sequence forwardSequence = DOTween.Sequence();
            forwardSequence.SetLink(_cameraTransform.gameObject);
            forwardSequence.Append(_cameraTransform
                                   .DOPath(_cameraWaypoints.ToArray(),
                                       _cameraMovementSettings.MoveAnimationDurationInSeconds,
                                       _cameraMovementSettings.MovePathType)
                                   .SetEase(_cameraMovementSettings.MoveEase)
                                   .SetLink(_cameraTransform.gameObject))
                           .Join(_cameraTransform
                                 .DODynamicLookAt(_cameraWaypoints.Last(),
                                     _cameraMovementSettings.MoveAnimationDurationInSeconds)
                                 .SetEase(_cameraMovementSettings.FinalRotateCameraEase))
                           .OnComplete(base.Appear);
        }

        public override void Disappear()
        {
            base.Disappear();
            if (_cameraWaypoints == null)
            {
                return;
            }

            var returnWaypoints = new List<Vector3>(_cameraWaypoints);
            returnWaypoints.Reverse();
            returnWaypoints.Add(_cameraOldPosition);
            float cameraLastWaypointLookInSeconds = _cameraMovementSettings.MoveAnimationDurationInSeconds -
                                                    _cameraMovementSettings
                                                        .FinalRotateCameraBeforeEndOfTheMovementDurationInSeconds;

            Sequence returnBackSequence = DOTween.Sequence();
            returnBackSequence.SetLink(_cameraTransform.gameObject);
            returnBackSequence
                .Append(_cameraTransform
                        .DOPath(returnWaypoints.ToArray(), _cameraMovementSettings.MoveAnimationDurationInSeconds,
                            _cameraMovementSettings.MovePathType)
                        .SetEase(_cameraMovementSettings.MoveEase))
                .Join(_cameraTransform.DODynamicLookAt(_cameraWaypoints.Last(), cameraLastWaypointLookInSeconds)
                                      .SetEase(_cameraMovementSettings.FinalRotateCameraEase)
                                      .OnComplete(() =>
                                          _cameraTransform
                                              .DORotate(_cameraOldRotation,
                                                  _cameraMovementSettings
                                                      .FinalRotateCameraBeforeEndOfTheMovementDurationInSeconds)
                                              .SetEase(_cameraMovementSettings.FinalRotateCameraEase)
                                              .SetLink(_cameraTransform.gameObject)));
        }
    }
}