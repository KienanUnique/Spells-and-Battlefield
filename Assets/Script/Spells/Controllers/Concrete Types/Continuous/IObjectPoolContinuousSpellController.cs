using Common.Abstract_Bases.Factories.Object_Pool;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IObjectPoolContinuousSpellController : IContinuousSpellController,
        IObjectPoolItem<IDataForActivationContinuousSpellObjectController>
    {
    }
}