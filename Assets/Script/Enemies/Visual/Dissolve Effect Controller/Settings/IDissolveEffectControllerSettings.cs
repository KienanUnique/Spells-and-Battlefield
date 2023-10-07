using DG.Tweening;

namespace Enemies.Visual.Dissolve_Effect_Controller.Settings
{
    public interface IDissolveEffectControllerSettings
    {
        public float DissolveAnimationDuration { get; }
        public Ease DissolveAnimationEase { get; }
    }
}