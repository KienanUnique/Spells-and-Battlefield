using DG.Tweening;

namespace Common.Dissolve_Effect_Controller
{
    public interface IDissolveEffectController
    {
        public void Appear(TweenCallback callback = null);
        public void Disappear(TweenCallback callback = null);
    }
}