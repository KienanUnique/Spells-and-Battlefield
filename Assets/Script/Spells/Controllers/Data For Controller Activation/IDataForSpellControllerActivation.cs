using Common.Readonly_Transform;
using Spells.Controllers.Data_For_Controller;

namespace Spells.Controllers.Data_For_Controller_Activation
{
    public interface
        IDataForSpellControllerActivation<out TConcreteSpellControllerData> : IBaseDataForSpellControllerActivation
        where TConcreteSpellControllerData : IInitializableDataForSpellControllerFromSetupScriptableObjects
    {
        TConcreteSpellControllerData ConcreteSpellControllerData { get; }
        ICaster Caster { get; }
        IReadonlyTransform CastPoint { get; }
    }
}