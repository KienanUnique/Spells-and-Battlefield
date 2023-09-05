using DG.Tweening;
using UnityEngine;

namespace UI.Element.View.Settings
{
    [CreateAssetMenu(fileName = "Default UI Element View Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Default UI Element View Settings",
        order = 0)]
    public class DefaultUIElementViewSettings : ScriptableObject, IDefaultUIElementViewSettings
    {
        [SerializeField] private float _scaleAnimationDuration = 0.4f;
        [SerializeField] private Ease _scaleAnimationEase = Ease.OutElastic;

        public float ScaleAnimationDuration => _scaleAnimationDuration;
        public Ease ScaleAnimationEase => _scaleAnimationEase;
    }
}