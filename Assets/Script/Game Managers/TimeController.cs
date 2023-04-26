using UnityEngine;

namespace Game_Managers
{
    public class TimeController : Singleton<TimeController>
    {
        [Range(0, 1f)] [SerializeField] private float _dashAimingTimeScaleRatio;
        protected override TimeController ThisInstance => this;

        public void SlowDownTimeForDashAiming()
        {
            Time.timeScale = _dashAimingTimeScaleRatio;
        }

        public void RestoreTimeSpeed()
        {
            Time.timeScale = 1f;
        }

        protected override void SpecialAwakeAction()
        {
        }
    }
}