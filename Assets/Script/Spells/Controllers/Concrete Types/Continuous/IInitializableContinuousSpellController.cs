using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Factory;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IInitializableContinuousSpellController : IContinuousSpellController
    {
        public void Initialize(IDataForContinuousSpellController spellControllerData, ICaster caster,
            IReadonlyTransform castPoint, ISpellObjectsFactory spellObjectsFactory);
    }
}