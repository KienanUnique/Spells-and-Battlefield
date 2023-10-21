using Common.Mechanic_Effects.Continuous_Effect;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Model
{
    public interface IContinuousEffectsPanelModel
    {
        public void HandleNewEffect(IAppliedContinuousEffectInformation effectInformation);
    }
}