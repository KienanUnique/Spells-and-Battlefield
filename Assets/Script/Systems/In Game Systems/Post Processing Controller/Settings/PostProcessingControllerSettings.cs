using DG.Tweening;
using UnityEngine;

namespace Systems.In_Game_Systems.Post_Processing_Controller.Settings
{
    [CreateAssetMenu(fileName = "Post Processing Controller Settings",
        menuName = ScriptableObjectsMenuDirectories.SystemsSettingsDirectory + "Post Processing Controller Settings",
        order = 0)]
    public class PostProcessingControllerSettings : ScriptableObject, IPostProcessingControllerSettings
    {
        [SerializeField] private float _applyDashEffectsVolumeDurationSeconds = 0.2f;
        [SerializeField] private float _applyDashAimingEffectsVolumeDurationSeconds = 0.5f;
        [SerializeField] private Ease _applyDashEffectsVolumeEase = Ease.OutCubic;

        public Ease ApplyEffectsVolumeEase => _applyDashEffectsVolumeEase;
        public float ApplyDashEffectsVolumeDurationSeconds => _applyDashEffectsVolumeDurationSeconds;

        public float ApplyDashAimingEffectsVolumeDurationSeconds => _applyDashAimingEffectsVolumeDurationSeconds;
    }
}