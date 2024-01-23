using Common.Abstract_Bases.Factories;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;
using Spells.Factory.Continuous;
using Spells.Factory.Instant;
using UnityEngine;
using Zenject;

namespace Spells.Factory
{
    public class SpellObjectsFactory : FactoryWithInstantiatorBase, ISpellObjectsFactory
    {
        private readonly IInstantSpellsFactory _instantSpellsFactory;
        private readonly IContinuousSpellsFactory _continuousSpellsFactory;

        private readonly PositionDataForInstantiation _defaultPositionDataForInstantiation =
            new PositionDataForInstantiation();

        public SpellObjectsFactory(IInstantiator instantiator, Transform defaultParentTransform) : base(instantiator,
            defaultParentTransform)
        {
            _instantSpellsFactory = new InstantSpellsFactory(instantiator, defaultParentTransform,
                _defaultPositionDataForInstantiation);
            _continuousSpellsFactory = new ContinuousSpellsFactory(instantiator, defaultParentTransform,
                _defaultPositionDataForInstantiation);
        }

        public IInstantSpellController Create(IDataForActivationInstantSpellObjectController dataForActivation,
            IInstantSpellPrefabProvider prefabProvider)
        {
            return _instantSpellsFactory.Create(dataForActivation, prefabProvider);
        }

        public IContinuousSpellController Create(IDataForActivationContinuousSpellObjectController dataForActivation,
            IContinuousSpellPrefabProvider prefabProvider)
        {
            return _continuousSpellsFactory.Create(dataForActivation, prefabProvider);
        }
    }
}