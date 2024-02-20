using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar.Settings
{
    [CreateAssetMenu(fileName = "Avatar View Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Avatar View Settings", order = 0)]
    public class AvatarViewSettings : ScriptableObject, IAvatarViewSettings
    {
        [SerializeField] private float _scaleAnimationDuration = 0.4f;
        [SerializeField] private Ease _scaleAnimationEase = Ease.OutElastic;

        public float ScaleAnimationDuration => _scaleAnimationDuration;

        public Ease ScaleAnimationEase => _scaleAnimationEase;
    }
}