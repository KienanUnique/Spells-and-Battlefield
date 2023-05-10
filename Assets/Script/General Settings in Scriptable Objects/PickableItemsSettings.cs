using DG.Tweening;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Pickable Items Settings",
        menuName = "Spells and Battlefield/Settings/General Settings/Pickable Items Settings", order = 0)]
    public class PickableItemsSettings : ScriptableObject
    {
        [SerializeField] private float _animationMinimumHeight = 1.6f;
        [SerializeField] private float _animationMaximumHeight = 2.6f;
        [SerializeField] private float _yAnimationDuration = 0.6f;
        [SerializeField] private float _rotateAnimationDuration = 1;
        [SerializeField] private float _appearScaleAnimationDuration = 3;
        [SerializeField] private float _disappearScaleAnimationDuration = 1;
        [SerializeField] private float _dropForce = 10f;
        [SerializeField] private Ease _sizeChangeEase = Ease.OutElastic;
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