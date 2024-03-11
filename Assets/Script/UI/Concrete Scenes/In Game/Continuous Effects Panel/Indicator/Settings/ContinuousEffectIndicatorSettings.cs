using DG.Tweening;
using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings
{
    [CreateAssetMenu(fileName = "Continuous Effect Indicator Settings",
        menuName =
            ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Continuous Effect Indicator Settings",
        order = 0)]
    public class ContinuousEffectIndicatorSettings : DefaultUIElementViewSettings, IContinuousEffectIndicatorSettings
    {
    }
}