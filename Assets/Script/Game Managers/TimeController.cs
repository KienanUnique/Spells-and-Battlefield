using Interfaces;
using UnityEngine;

namespace Game_Managers
{
    public class TimeController : Singleton<TimeController>
    {
        [Range(0, 1f)] [SerializeField] private float _dashAimingTimeScaleRatio;
        private IPlayer _player;

        protected override void SpecialAwakeAction()
        {
            _player = PlayerProvider.Instance.Player;
        }

        private void OnEnable()
        {
            _player.DashAiming += SlowDownTimeForDashAiming;
            _player.Dashed += RestoreTimeSpeed;
        }

        private void OnDisable()
        {
            _player.DashAiming -= SlowDownTimeForDashAiming;
            _player.Dashed -= RestoreTimeSpeed;
        }

        private void SlowDownTimeForDashAiming()
        {
            Time.timeScale = _dashAimingTimeScaleRatio;
        }

        private void RestoreTimeSpeed()
        {
            Time.timeScale = 1f;
        }
    }
}