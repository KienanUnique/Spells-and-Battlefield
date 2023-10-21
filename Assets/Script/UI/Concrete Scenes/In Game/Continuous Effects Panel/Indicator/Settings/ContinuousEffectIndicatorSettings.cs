using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings
{
    [CreateAssetMenu(fileName = "Continuous Effect Indicator Settings",
        menuName =
            ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Continuous Effect Indicator Settings",
        order = 0)]
    public class ContinuousEffectIndicatorSettings : ScriptableObject, IContinuousEffectIndicatorSettings
    {
        [SerializeField] private float _scaleAnimationDuration;
        [SerializeField] private Ease _scaleAnimationEase;

        public float ScaleAnimationDuration => _scaleAnimationDuration;
        public Ease ScaleAnimationEase => _scaleAnimationEase;
    }
}