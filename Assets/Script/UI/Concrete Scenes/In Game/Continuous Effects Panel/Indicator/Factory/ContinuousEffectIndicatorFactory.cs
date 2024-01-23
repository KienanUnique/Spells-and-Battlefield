using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Data_For_Activation;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Presenter;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings;
using UnityEngine;
using Zenject;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Factory
{
    public class ContinuousEffectIndicatorFactory : ObjectPoolingFactoryWithActivation<
            IContinuousEffectIndicatorPresenter, IContinuousEffectIndicatorDataForActivation>,
        IContinuousEffectIndicatorFactory
    {
        public ContinuousEffectIndicatorFactory(IInstantiator instantiator, Transform defaultParentTransform,
            IContinuousEffectIndicatorFactorySettings settings) : base(instantiator, defaultParentTransform,
            settings.ObjectPooledIndicatorsCount, settings.PrefabProvider,
            new PositionDataForInstantiation(Vector3.zero, Quaternion.identity))
        {
        }

        public void Create(IContinuousEffectIndicatorDataForActivation dataForActivation)
        {
            CreateItem(dataForActivation);
        }
    }
}