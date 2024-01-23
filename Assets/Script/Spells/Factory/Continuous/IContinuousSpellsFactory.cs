using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;

namespace Spells.Factory.Continuous
{
    public interface IContinuousSpellsFactory
    {
        public IContinuousSpellController Create(IDataForActivationContinuousSpellObjectController dataForActivation,
            IContinuousSpellPrefabProvider prefabProvider, IPositionDataForInstantiation positionDataForInstantiation);
    }
}