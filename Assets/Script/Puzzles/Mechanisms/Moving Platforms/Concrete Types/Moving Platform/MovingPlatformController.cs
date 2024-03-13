using System.Collections.Generic;
using DG.Tweening;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform
{
    [RequireComponent(typeof(MovingPlatformControllerSetup))]
    public class MovingPlatformController : MovingPlatformWithStickingBase, IInitializableMovingPlatformController
    {
        private bool _needMoveBackward;

        public void Initialize(List<IMechanismsTrigger> triggers,
            IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            AddTriggers(triggers);
            base.Initialize(dataForControllerBase);
            SetInitializedStatus();
        }

        protected override void StartJob()
        {
            ParentObjectToMove.DOKill();

            var path = new List<Vector3>(Waypoints);
            if (_needMoveBackward)
            {
                path.Reverse();
            }

            ParentObjectToMove.DOPath(path.ToArray(), MovementSpeed, Settings.MovementPathType)
                              .SetSpeedBased()
                              .SetEase(Settings.MovementEase)
                              .SetLink(gameObject)
                              .OnKill(HandleDoneJob);

            _needMoveBackward = !_needMoveBackward;
        }
    }
}