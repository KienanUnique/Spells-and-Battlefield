using Common.Interfaces;
using Common.Mechanic_Effects.Continuous_Effect.Information_For_Creation;

namespace Common.Mechanic_Effects.Continuous_Effect.Factory
{
    public class ContinuousEffectFactory : IContinuousEffectFactory
    {
        private readonly IContinuousEffectInformationForCreation _informationForCreation;

        public ContinuousEffectFactory(IContinuousEffectInformationForCreation informationForCreation)
        {
            _informationForCreation = informationForCreation;
        }

        public IContinuousEffect Create(IInteractable target)
        {
            return new ContinuousEffect(_informationForCreation, target);
        }
    }
}