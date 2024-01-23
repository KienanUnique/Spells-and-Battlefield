using System;
using Common.Abstract_Bases.Factories;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;
using UnityEngine;
using Zenject;
using IPrefabProvider = Common.IPrefabProvider;

namespace Spells.Factory.Instant
{
    public class InstantSpellsFactory : FactoriesDictionary<IPrefabProvider, IConcreteInstantSpellFactory>,
        IInstantSpellsFactory
    {
        private const int StartItemsCount = 1;
        private readonly IPositionDataForInstantiation _defaultPositionDataForInstantiation;

        public InstantSpellsFactory(IInstantiator instantiator, Transform parentTransform,
            IPositionDataForInstantiation defaultPositionDataForInstantiation) : base(instantiator, parentTransform)
        {
            _defaultPositionDataForInstantiation = defaultPositionDataForInstantiation;
        }

        public IInstantSpellController Create(IDataForActivationInstantSpellObjectController dataForActivation,
            IInstantSpellPrefabProvider prefabProvider)
        {
            throw new NotImplementedException();
        }

        protected override IConcreteInstantSpellFactory CreateFactoryForKey(IPrefabProvider key,
            IInstantiator instantiator, Transform parentTransform)
        {
            return new ConcreteInstantSpellFactory(instantiator, parentTransform, StartItemsCount, key,
                _defaultPositionDataForInstantiation);
        }
    }
}