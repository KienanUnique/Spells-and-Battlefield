using Interfaces;
using UnityEngine;
using Zenject;

namespace Systems.Time_Controller
{
    public class TimeController : MonoBehaviour, ITimeController
    {
        [Range(0, 1f)] [SerializeField] private float _dashAimingTimeScaleRatio = 0.1f;
        private float _timeScaleBeforePause = 1f;
        private IPlayerInformationProvider _playerInformationProvider;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider)
        {
            _playerInformationProvider = playerInformationProvider;
        }

        public void StopTime()
        {
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0f;
        }

        public void RestoreTimeToPrevious()
        {
            Time.timeScale = _timeScaleBeforePause;
        }

        public void RestoreTimeToNormal()
        {
            Time.timeScale = 1f;
        }

        private void OnEnable()
        {
            _playerInformationProvider.DashAiming += SlowDownTimeForDashAiming;
            _playerInformationProvider.Dashed += RestoreTimeToNormal;
        }

        private void OnDisable()
        {
            _playerInformationProvider.DashAiming -= SlowDownTimeForDashAiming;
            _playerInformationProvider.Dashed -= RestoreTimeToNormal;
        }

        private void SlowDownTimeForDashAiming()
        {
            Time.timeScale = _dashAimingTimeScaleRatio;
        }
    }
}