using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;

namespace Spells.Factory.Instant
{
    public interface IInstantSpellsFactory
    {
        public IInstantSpellController Create(IDataForActivationInstantSpellObjectController dataForActivation,
            IInstantSpellPrefabProvider prefabProvider, IPositionDataForInstantiation positionDataForInstantiation);
    }
}