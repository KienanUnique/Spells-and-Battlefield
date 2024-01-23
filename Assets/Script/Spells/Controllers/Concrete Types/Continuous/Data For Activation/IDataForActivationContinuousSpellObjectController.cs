using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Controllers.Data_For_Controller_Activation;

namespace Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation
{
    public interface IDataForActivationContinuousSpellObjectController : IDataForSpellControllerActivation<
        IDataForContinuousSpellControllerFromSetupScriptableObjects>
    {
    }
}