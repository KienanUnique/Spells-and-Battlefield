using Common.Abstract_Bases.Initializable_MonoBehaviour;
using DG.Tweening;
using Player;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace Systems.In_Game_Systems.Post_Processing_Controller
{
    public class PostProcessingController : InitializableMonoBehaviourBase, IInitializablePostProcessingController
    {
        private const float DisabledVolumeWeight = 0f;
        private const float EnabledVolumeWeight = 1f;

        private Volume _dashEffectsVolume;
        private Volume _dashAimingEffectsVolume;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPostProcessingControllerSettings _settings;
        private GameObject _gameObjectToLink;

        public void Initialize(IPlayerInformationProvider playerInformationProvider, Volume dashEffectsVolume,
            Volume dashAimingEffectsVolume, IPostProcessingControllerSettings settings)
        {
            _playerInformationProvider = playerInformationProvider;
            _dashEffectsVolume = dashEffectsVolume;
            _dashAimingEffectsVolume = dashAimingEffectsVolume;
            _settings = settings;
            _gameObjectToLink = gameObject;
            SetInitializedStatus();
            _dashEffectsVolume.weight = 0;
            _dashEffectsVolume.enabled = false;
        }

        protected override void SubscribeOnEvents()
        {
            _playerInformationProvider.DashAiming += AppearDashAimingEffects;
            _playerInformationProvider.DashAimingCanceled += DisappearDashAimingEffects;
            _playerInformationProvider.Dashed += PlayDashEffects;
        }

        protected override void UnsubscribeFromEvents()
        {
            _playerInformationProvider.DashAiming -= AppearDashAimingEffects;
            _playerInformationProvider.DashAimingCanceled -= DisappearDashAimingEffects;
            _playerInformationProvider.Dashed -= PlayDashEffects;
        }

        private void AppearDashAimingEffects()
        {
            EnableVolume(_dashAimingEffectsVolume, _settings.ApplyDashAimingEffectsVolumeDurationSeconds);
        }

        private void DisappearDashAimingEffects()
        {
            DisableVolume(_dashAimingEffectsVolume, _settings.ApplyDashAimingEffectsVolumeDurationSeconds);
        }

        private void PlayDashEffects()
        {
            EnableVolume(_dashEffectsVolume, _settings.ApplyDashEffectsVolumeDurationSeconds)
                .OnComplete(() => DisableVolume(_dashEffectsVolume, _settings.ApplyDashEffectsVolumeDurationSeconds));
        }

        private Tween EnableVolume(Volume volume, float transitionAnimationDuration)
        {
            volume.enabled = true;
            return ChangeVolumeWeight(volume, transitionAnimationDuration, volume.weight, EnabledVolumeWeight);
        }

        private Tween DisableVolume(Volume volume, float transitionAnimationDuration)
        {
            return ChangeVolumeWeight(volume, transitionAnimationDuration, volume.weight, DisabledVolumeWeight)
                .OnComplete(() => volume.enabled = false);
        }

        private Tween ChangeVolumeWeight(Volume volume, float transitionAnimationDuration, float startValue,
            float endValue)
        {
            volume.enabled = true;
            return DOVirtual
                   .Float(startValue, endValue, transitionAnimationDuration,
                       currentWeight => volume.weight = currentWeight)
                   .SetLink(gameObject)
                   .SetEase(_settings.ApplyEffectsVolumeEase)
                   .SetLink(_gameObjectToLink);
        }
    }
}