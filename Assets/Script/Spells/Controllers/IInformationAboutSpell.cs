using Common;
using Enemies.Look_Point_Calculator;

namespace Spells.Controllers
{
    public interface IInformationAboutSpell<out TDataForActivation, out TAnimationData, out TPrefabProvider>
        where TPrefabProvider : IPrefabProvider
    {
        public TAnimationData AnimationData { get; }
        public TPrefabProvider PrefabProvider { get; }
        public ILookPointCalculator LookPointCalculator { get; }
    }
}