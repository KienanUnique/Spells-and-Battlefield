using Common.Abstract_Bases.Factories;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;
using UnityEngine;
using Zenject;
using IPrefabProvider = Common.IPrefabProvider;

namespace Spells.Factory.Continuous
{
    public class ContinuousSpellsFactory : FactoriesDictionary<IPrefabProvider, IConcreteContinuousSpellFactory>,
        IContinuousSpellsFactory
    {
        private const int StartItemsCount = 1;
        private readonly IPositionDataForInstantiation _defaultPositionDataForInstantiation;

        public ContinuousSpellsFactory(IInstantiator instantiator, Transform parentTransform,
            IPositionDataForInstantiation defaultPositionDataForInstantiation) : base(instantiator, parentTransform)
        {
            _defaultPositionDataForInstantiation = defaultPositionDataForInstantiation;
        }

        public IContinuousSpellController Create(IDataForActivationContinuousSpellObjectController dataForActivation,
            IContinuousSpellPrefabProvider prefabProvider, IPositionDataForInstantiation positionDataForInstantiation)
        {
            IConcreteContinuousSpellFactory factory = GetFactoryByKey(prefabProvider);
            return factory.Create(dataForActivation);
        }

        protected override IConcreteContinuousSpellFactory CreateFactoryForKey(IPrefabProvider key,
            IInstantiator instantiator, Transform parentTransform)
        {
            return new ConcreteContinuousSpellFactory(instantiator, parentTransform, StartItemsCount, key,
                _defaultPositionDataForInstantiation);
        }
    }
}