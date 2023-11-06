using UnityEngine;

namespace Player.Camera_Effects.Camera_Particle_System_Controller
{
    public class PlayerCameraParticleSystemController : IPlayerCameraParticleSystemController
    {
        private readonly IPlayerCameraParticleSystemSettings _settings;
        private ParticleSystem.EmissionModule _overSpeedEffectEmission;

        public PlayerCameraParticleSystemController(IPlayerCameraParticleSystemSettings settings,
            ParticleSystem overSpeedParticleSystem)
        {
            _settings = settings;
            _overSpeedEffectEmission = overSpeedParticleSystem.emission;
            UpdateOverSpeedValue(0);
        }

        public void UpdateOverSpeedValue(float newOverSpeedValue)
        {
            float rateOverTime;
            if (newOverSpeedValue <= _settings.MinimumOverSpeedValueForWindTrailEffect)
            {
                rateOverTime = 0f;
            }
            else if (newOverSpeedValue >= _settings.MaximumOverSpeedValueForWindTrailEffect)
            {
                rateOverTime = 1f;
            }
            else
            {
                rateOverTime = newOverSpeedValue / _settings.MaximumOverSpeedValueForWindTrailEffect;
            }

            _overSpeedEffectEmission.rateOverTime = _settings.OverSpeedMaximumEmissionRateOverTime * rateOverTime;
        }
    }
}