using DG.Tweening;

namespace Common.Dissolve_Effect_Controller.Settings
{
    public interface IDissolveEffectControllerSettings
    {
        public float DissolveAnimationDuration { get; }
        public Ease DissolveAnimationEase { get; }
    }
}