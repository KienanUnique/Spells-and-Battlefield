using DG.Tweening;
using UnityEngine;

namespace Settings.Puzzles.Triggers
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.PuzzleTriggersDirectory + "Pressure Plate Settings",
        fileName = "Pressure Plate Settings", order = 0)]
    public class PlateSettings : ScriptableObject
    {
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Ease _animationEase = Ease.OutQuart;
        [SerializeField] private float _pressedScaleY = 0.01f;
        [SerializeField] private float _unpressedScaleY = 0.1f;
        
        public float AnimationDuration => _animationDuration;
        public Ease AnimationEase => _animationEase;
        public float PressedScaleY => _pressedScaleY;
        public float UnpressedScaleY => _unpressedScaleY;
    }
}