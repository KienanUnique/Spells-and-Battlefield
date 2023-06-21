using DG.Tweening;
using UnityEngine;

namespace Settings.UI
{
    [CreateAssetMenu(fileName = "UI Animation Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "UI Animation Settings", order = 0)]
    public class GeneralUIAnimationSettings : ScriptableObject
    {
        [SerializeField] private float _scaleAnimationDuration = 0.4f;
        [SerializeField] private Ease _scaleAnimationEase = Ease.OutElastic;

        public float ScaleAnimationDuration => _scaleAnimationDuration;
        public Ease ScaleAnimationEase => _scaleAnimationEase;
    }
}