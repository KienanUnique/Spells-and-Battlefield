namespace Player.Camera_Effects.Camera_Particle_System_Controller
{
    public interface IPlayerCameraParticleSystemSettings
    {
        public float OverSpeedMaximumEmissionRateOverTime { get; }
        public float MaximumOverSpeedValueForWindTrailEffect { get; }
        public float MinimumOverSpeedValueForWindTrailEffect { get; }
    }
}