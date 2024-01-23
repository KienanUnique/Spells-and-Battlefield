using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;

namespace Spells.Factory.Continuous
{
    public interface IConcreteContinuousSpellFactory
    {
        public IContinuousSpellController Create(IDataForActivationContinuousSpellObjectController dataForActivation);
    }
}