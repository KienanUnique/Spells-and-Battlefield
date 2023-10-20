using System;
using Common.Mechanic_Effects.Source;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Model
{
    public interface IDamageIndicatorModel
    {
        public event Action<float> NeedIndicateAboutExternalDamage;
        public event Action NeedIndicateAboutLocalDamage;
        public void HandleDamageSourceInformation(IEffectSourceInformation sourceInformation);
    }
}