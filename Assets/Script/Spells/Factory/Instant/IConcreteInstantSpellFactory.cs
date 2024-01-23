using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;

namespace Spells.Factory.Instant
{
    public interface IConcreteInstantSpellFactory
    {
        public IInstantSpellController Create(IDataForActivationInstantSpellObjectController dataForActivation);
    }
}