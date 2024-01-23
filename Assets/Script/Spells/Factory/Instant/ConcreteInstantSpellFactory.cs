using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;
using UnityEngine;
using Zenject;
using IPrefabProvider = Common.IPrefabProvider;

namespace Spells.Factory.Instant
{
    public class ConcreteInstantSpellFactory : ObjectPoolingFactoryWithActivation<IObjectPoolInstantSpellController,
            IDataForActivationInstantSpellObjectController>,
        IConcreteInstantSpellFactory
    {
        public ConcreteInstantSpellFactory(IInstantiator instantiator, Transform defaultParentTransform,
            int needItemsCount, IPrefabProvider prefabProvider,
            IPositionDataForInstantiation defaultPositionDataForInstantiation) : base(instantiator,
            defaultParentTransform, needItemsCount, prefabProvider, defaultPositionDataForInstantiation)
        {
        }

        public IInstantSpellController Create(IDataForActivationInstantSpellObjectController dataForActivation)
        {
            return CreateItem(dataForActivation);
        }
    }
}