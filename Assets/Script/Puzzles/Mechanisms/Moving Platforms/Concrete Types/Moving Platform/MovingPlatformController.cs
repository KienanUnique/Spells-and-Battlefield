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
        private List<IMechanismsTrigger> _triggers;

        public void Initialize(List<IMechanismsTrigger> triggers,
            IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _triggers = triggers;
            base.Initialize(dataForControllerBase);
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            foreach (IMechanismsTrigger trigger in _triggers)
            {
                trigger.Triggered += OnTriggered;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            foreach (IMechanismsTrigger trigger in _triggers)
            {
                trigger.Triggered -= OnTriggered;
            }
        }

        private void OnTriggered()
        {
            if (_isTriggersDisabled)
            {
                return;
            }

            _isTriggersDisabled = true;
            _parentObjectToMove.DOKill();
            var path = new List<Vector3>(_waypoints);
            if (_needMoveBackward)
            {
                path.Reverse();
            }

            _parentObjectToMove.DOPath(path.ToArray(), _movementSpeed, _settings.MovementPathType)
                               .ApplyCustomSetupForMovingPlatforms(gameObject, _settings, _delayInSeconds)
                               .OnKill(() => _isTriggersDisabled = false);

            _needMoveBackward = !_needMoveBackward;
        }
    }
}