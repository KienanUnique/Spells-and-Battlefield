using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Controllers.Data_For_Controller;
using Spells.Controllers.Data_For_Controller_Activation;

namespace Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation
{
    public class DataForActivationContinuousSpellObjectController : DataForSpellControllerActivation<
        IDataForContinuousSpellControllerFromSetupScriptableObjects>
    {
        public DataForActivationContinuousSpellObjectController(
            IDataForContinuousSpellControllerFromSetupScriptableObjects concreteSpellControllerData, ICaster caster,
            IBaseDataForSpellController controllerData, IReadonlyTransform castPoint) : base(
            concreteSpellControllerData, caster, controllerData, castPoint)
        {
        }
    }
}