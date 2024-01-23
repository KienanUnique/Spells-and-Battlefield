using Common.Abstract_Bases.Factories.Object_Pool;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;

namespace Spells.Controllers.Concrete_Types.Instant
{
    public interface IObjectPoolInstantSpellController : IInstantSpellController,
        IObjectPoolItem<IDataForActivationInstantSpellObjectController>
    {
    }
}