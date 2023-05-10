using System;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Managers.Time_Controller
{
    public class TimeController : MonoBehaviour, ITimeController
    {
        [Range(0, 1f)] [SerializeField] private float _dashAimingTimeScaleRatio = 0.1f;
        private float _timeScaleBeforePause = 1f;
        private IPlayerInformation _playerInformation;

        [Inject]
        private void Construct(IPlayerInformation playerInformation)
        {
            _playerInformation = playerInformation;
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
            _playerInformation.DashAiming += SlowDownTimeForDashAiming;
            _playerInformation.Dashed += RestoreTimeToNormal;
        }

        private void OnDisable()
        {
            _playerInformation.DashAiming -= SlowDownTimeForDashAiming;
            _playerInformation.Dashed -= RestoreTimeToNormal;
        }

        private void SlowDownTimeForDashAiming()
        {
            Time.timeScale = _dashAimingTimeScaleRatio;
        }
    }
}