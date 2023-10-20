using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Settings
{
    [CreateAssetMenu(fileName = "Damage Indicator Element Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Damage Indicator Element Settings",
        order = 0)]
    public class DamageIndicatorElementSettings : ScriptableObject, IDamageIndicatorElementSettings
    {
        [SerializeField] private float _appearFadeDuration = 0.1f;
        [SerializeField] private Ease _appearFadeEase = Ease.OutCubic;
        [SerializeField] private float _disappearFadeDuration = 1f;
        [SerializeField] private Ease _disappearFadeEase = Ease.OutCubic;

        public float AppearFadeDuration => _appearFadeDuration;
        public Ease AppearFadeEase => _appearFadeEase;
        public float DisappearFadeDuration => _disappearFadeDuration;
        public Ease DisappearFadeEase => _disappearFadeEase;
    }
}