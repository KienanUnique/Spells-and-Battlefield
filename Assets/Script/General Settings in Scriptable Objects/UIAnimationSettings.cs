using DG.Tweening;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [CreateAssetMenu(fileName = "UI Animation Settings",
        menuName = "Spells and Battlefield/General Settings/UI Animation Settings", order = 0)]
    public class UIAnimationSettings : ScriptableObject
    {
        [SerializeField] private float _scaleAnimationDuration;
        [SerializeField] private Ease _scaleAnimationEase;

        public float ScaleAnimationDuration => _scaleAnimationDuration;
        public Ease ScaleAnimationEase => _scaleAnimationEase;
    }
}