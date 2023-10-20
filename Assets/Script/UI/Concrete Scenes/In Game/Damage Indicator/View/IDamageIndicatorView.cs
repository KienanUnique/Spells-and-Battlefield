using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.View
{
    public interface IDamageIndicatorView : IUIElementView
    {
        public void IndicateAboutExternalDamage(float screenSpaceDamageAngle);
        public void IndicateAboutLocalDamage();
    }
}