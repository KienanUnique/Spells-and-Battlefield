using Common;
using Enemies.Look_Point_Calculator;
using Spells.Controllers.Data_For_Controller;

namespace Spells.Controllers
{
    public interface IInformationAboutSpell<out TDataForController, out TAnimationData, out TPrefabProvider>
        where TPrefabProvider : IPrefabProvider where TDataForController : IDataForSpellController
    {
        public TDataForController DataForController { get; }
        public TAnimationData AnimationData { get; }
        public TPrefabProvider PrefabProvider { get; }
        public ILookPointCalculator LookPointCalculator { get; }
    }
}