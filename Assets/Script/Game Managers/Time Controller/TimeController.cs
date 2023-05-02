using UnityEngine;

namespace Game_Managers.Time_Controller
{
    public class TimeController : MonoBehaviour, ITimeController
    {
        [Range(0, 1f)] [SerializeField] private float _dashAimingTimeScaleRatio = 0.1f;
        private float _timeScaleBeforePause = 1f;

        public void StopTime()
        {
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0f;
        }

        public void RestoreTimeToPrevious()
        {
            Time.timeScale = _timeScaleBeforePause;
        }

        public void SlowDownTimeForDashAiming()
        {
            Time.timeScale = _dashAimingTimeScaleRatio;
        }

        public void RestoreTimeToNormal()
        {
            Time.timeScale = 1f;
        }
    }
}