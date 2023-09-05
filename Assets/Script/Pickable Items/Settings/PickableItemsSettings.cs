using DG.Tweening;
using UnityEngine;

namespace Pickable_Items.Settings
{
    [CreateAssetMenu(fileName = "Pickable Items Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Pickable Items Settings", order = 0)]
    public class PickableItemsSettings : ScriptableObject, IPickableItemsSettings
    {
        [SerializeField] private float _animationMinimumHeight = 1f;
        [SerializeField] private float _animationMaximumHeight = 1.2f;
        [SerializeField] private float _yAnimationDuration = 1f;
        [SerializeField] private float _rotateAnimationDuration = 1;
        [SerializeField] private float _appearScaleAnimationDuration = 0.5f;
        [SerializeField] private float _disappearScaleAnimationDuration = 0.5f;
        [SerializeField] private float _dropForce = 10f;
        [SerializeField] private Ease _sizeChangeEase = Ease.InElastic;
        [SerializeField] private Ease _rotatingEase = Ease.Linear;
        [SerializeField] private Ease _yMovingEase = Ease.InOutCubic;

        public float AnimationMinimumHeight => _animationMinimumHeight;
        public float AnimationMaximumHeight => _animationMaximumHeight;
        public float YAnimationDuration => _yAnimationDuration;
        public float RotateAnimationDuration => _rotateAnimationDuration;
        public float AppearScaleAnimationDuration => _appearScaleAnimationDuration;
        public float DisappearScaleAnimationDuration => _disappearScaleAnimationDuration;
        public float DropForce => _dropForce;
        public Ease SizeChangeEase => _sizeChangeEase;
        public Ease RotatingEase => _rotatingEase;
        public Ease YMovingEase => _yMovingEase;
    }
}