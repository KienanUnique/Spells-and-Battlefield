using DG.Tweening;
using UnityEngine;

namespace UI.Popup_Text.Settings
{
    [CreateAssetMenu(fileName = "Popup Text Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Popup Text Settings", order = 0)]
    public class PopupTextSettings : ScriptableObject, IPopupTextSettings
    {
        [SerializeField] private float _animationDurationInSeconds = 4f;
        [SerializeField] private float _moveMinimumRadius = 2f;
        [SerializeField] private float _moveMaximumRadius = 7f;
        [SerializeField] private Ease _movementEase;
        [SerializeField] private Ease _scaleEase;

        public Ease MovementEase => _movementEase;
        public float MoveMaximumRadius => _moveMaximumRadius;
        public float MoveMinimumRadius => _moveMinimumRadius;
        public float AnimationDurationInSeconds => _animationDurationInSeconds;
        public Ease ScaleEase => _scaleEase;
    }
}