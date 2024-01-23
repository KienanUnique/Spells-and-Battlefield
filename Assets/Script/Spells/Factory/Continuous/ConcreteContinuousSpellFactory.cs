using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using UnityEngine;
using Zenject;
using IPrefabProvider = Common.IPrefabProvider;

namespace Spells.Factory.Continuous
{
    public class ConcreteContinuousSpellFactory :
        ObjectPoolingFactoryWithActivation<IObjectPoolContinuousSpellController,
            IDataForActivationContinuousSpellObjectController>,
        IConcreteContinuousSpellFactory
    {
        public ConcreteContinuousSpellFactory(IInstantiator instantiator, Transform defaultParentTransform,
            int needItemsCount, IPrefabProvider prefabProvider,
            IPositionDataForInstantiation defaultPositionDataForInstantiation) : base(instantiator,
            defaultParentTransform, needItemsCount, prefabProvider, defaultPositionDataForInstantiation)
        {
        }

        public IContinuousSpellController Create(IDataForActivationContinuousSpellObjectController dataForActivation)
        {
            return CreateItem(dataForActivation);
        }
    }
}