using Common.Abstract_Bases.Initializable_MonoBehaviour;
using DG.Tweening;
using Player;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine.Rendering;

namespace Systems.In_Game_Systems.Post_Processing_Controller
{
    public class PostProcessingController : InitializableMonoBehaviourBase, IInitializablePostProcessingController
    {
        private Volume _dashEffectsVolume;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPostProcessingControllerSettings _settings;

        public void Initialize(IPlayerInformationProvider playerInformationProvider, Volume dashEffectsVolume,
            IPostProcessingControllerSettings settings)
        {
            _playerInformationProvider = playerInformationProvider;
            _dashEffectsVolume = dashEffectsVolume;
            _settings = settings;
            SetInitializedStatus();
            _dashEffectsVolume.weight = 0;
            _dashEffectsVolume.enabled = false;
        }

        protected override void SubscribeOnEvents()
        {
            _playerInformationProvider.Dashed += PlayDashEffects;
        }

        protected override void UnsubscribeFromEvents()
        {
            _playerInformationProvider.Dashed -= PlayDashEffects;
        }

        private void PlayDashEffects()
        {
            _dashEffectsVolume.enabled = true;
            DOVirtual
                .Float(0, 1, _settings.ApplyDashEffectsVolumeDurationSeconds,
                    currentWeight => _dashEffectsVolume.weight = currentWeight)
                .SetLink(gameObject)
                .SetEase(_settings.ApplyDashEffectsVolumeEase)
                .OnComplete(() =>
                    DOVirtual.Float(1, 0, _settings.ApplyDashEffectsVolumeDurationSeconds,
                                 currentWeight => _dashEffectsVolume.weight = currentWeight)
                             .SetLink(gameObject)
                             .SetEase(_settings.ApplyDashEffectsVolumeEase)
                             .OnComplete(() => _dashEffectsVolume.enabled = false));
        }
    }
}