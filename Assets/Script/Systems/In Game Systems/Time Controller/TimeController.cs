using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Interfaces;
using Systems.In_Game_Systems.Time_Controller.Settings;
using UnityEngine;

namespace Systems.In_Game_Systems.Time_Controller
{
    public class TimeController : InitializableMonoBehaviourBase, ITimeController, IInitializableTimeController
    {
        private IPlayerInformationProvider _playerInformationProvider;
        private ITimeControllerSettings _settings;
        private float _timeScaleBeforePause = 1f;

        public void Initialize(IPlayerInformationProvider playerInformationProvider, ITimeControllerSettings settings)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            SetInitializedStatus();
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

        protected override void SubscribeOnEvents()
        {
            _playerInformationProvider.DashAiming += SlowDownTimeForDashAiming;
            _playerInformationProvider.Dashed += RestoreTimeToNormal;
        }

        protected override void UnsubscribeFromEvents()
        {
            _playerInformationProvider.DashAiming -= SlowDownTimeForDashAiming;
            _playerInformationProvider.Dashed -= RestoreTimeToNormal;
        }

        private void SlowDownTimeForDashAiming()
        {
            Time.timeScale = _settings.DashAimingTimeScaleRatio;
        }
    }
}