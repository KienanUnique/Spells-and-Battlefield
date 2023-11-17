using Common.Readonly_Transform;
using Spells.Factory;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IInitializableContinuousSpellController : IContinuousSpellController
    {
        public void Initialize(IDataForContinuousSpellController spellControllerData, ICaster caster,
            IReadonlyTransform castPoint, ISpellObjectsFactory spellObjectsFactory);
    }
}