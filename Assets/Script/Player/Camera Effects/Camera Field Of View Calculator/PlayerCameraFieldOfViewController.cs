using System.Collections;
using Common.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Player.Camera_Effects.Camera_Field_Of_View_Calculator
{
    public class PlayerCameraFieldOfViewController : IPlayerCameraFieldOfViewController
    {
        private readonly IPlayerCameraFOVSettings _settings;
        private readonly Camera _camera;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly GameObject _gameObjectToLink;
        private float _currentTargetFOV;
        private Coroutine _updateOverSpeedRatioCoroutine;
        private Sequence _dashAnimationSequence;

        public PlayerCameraFieldOfViewController(IPlayerCameraFOVSettings settings, Camera camera,
            ICoroutineStarter coroutineStarter)
        {
            _settings = settings;
            _camera = camera;
            _coroutineStarter = coroutineStarter;
            _gameObjectToLink = _camera.gameObject;
            _currentTargetFOV = _settings.BaseFOV;
            StartUpdatingOverSpeedRatioContinuously();
        }

        public void UpdateOverSpeedValue(float newOverSpeedValue)
        {
            float overSpeedRatio;
            if (newOverSpeedValue == 0f)
            {
                overSpeedRatio = 0f;
            }
            else if (newOverSpeedValue >= _settings.MaximumEffectsOverSpeedValue)
            {
                overSpeedRatio = 1f;
            }
            else
            {
                overSpeedRatio = newOverSpeedValue / _settings.MaximumEffectsOverSpeedValue;
            }

            _currentTargetFOV =
                _settings.BaseFOV + _settings.MaximumOverSpeedFieldOfViewAdditionalValue * overSpeedRatio;
        }

        public void PlayIncreaseFieldOfViewAnimation()
        {
            _dashAnimationSequence?.Kill();
            StopUpdatingOverSpeedRatioContinuously();
            _dashAnimationSequence = DOTween.Sequence();
            _dashAnimationSequence.Append(_camera
                                          .DOFieldOfView(_settings.DashFOV, _settings.OnDashFOVChangeAnimationDuration)
                                          .SetEase(_settings.ChangeCameraFOVAnimationEase));
            _dashAnimationSequence.Append(_camera
                                          .DOFieldOfView(_currentTargetFOV, _settings.OnDashFOVChangeAnimationDuration)
                                          .SetEase(_settings.ChangeCameraFOVAnimationEase));
            _dashAnimationSequence.SetLink(_gameObjectToLink);

            _dashAnimationSequence.OnComplete(() =>
            {
                _dashAnimationSequence = null;
                StartUpdatingOverSpeedRatioContinuously();
            });
        }

        private void StartUpdatingOverSpeedRatioContinuously()
        {
            if (_updateOverSpeedRatioCoroutine != null)
            {
                return;
            }

            _updateOverSpeedRatioCoroutine = _coroutineStarter.StartCoroutine(UpdateOverSpeedRatioContinuously());
        }

        private void StopUpdatingOverSpeedRatioContinuously()
        {
            if (_updateOverSpeedRatioCoroutine == null)
            {
                return;
            }

            _coroutineStarter.StopCoroutine(_updateOverSpeedRatioCoroutine);
            _updateOverSpeedRatioCoroutine = null;
        }

        private IEnumerator UpdateOverSpeedRatioContinuously()
        {
            while (true)
            {
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _currentTargetFOV,
                    _settings.OverSpeedFOVChangeSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}