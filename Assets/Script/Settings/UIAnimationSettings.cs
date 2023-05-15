using DG.Tweening;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "UI Animation Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "UI Animation Settings", order = 0)]
    public class UIAnimationSettings : ScriptableObject
    {
        [SerializeField] private float _scaleAnimationDuration;
        [SerializeField] private Ease _scaleAnimationEase;

        public float ScaleAnimationDuration => _scaleAnimationDuration;
        public Ease ScaleAnimationEase => _scaleAnimationEase;
    }
}