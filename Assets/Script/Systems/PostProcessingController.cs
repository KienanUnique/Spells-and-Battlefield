using Common;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Systems
{
    public class PostProcessingController : Singleton<PostProcessingController>
    {
        [SerializeField] private Volume _dashEffectsVolume;
        [SerializeField] private float _applyDashEffectsVolumeDurationSeconds = 0.2f;
        [SerializeField] private Ease _applyDashEffectsVolumeEase = Ease.OutCubic;

        private IPlayerInformationProvider _playerInformationProvider;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider)
        {
            _playerInformationProvider = playerInformationProvider;
        }

        private void OnEnable()
        {
            _playerInformationProvider.Dashed += PlayDashEffects;
        }

        private void OnDisable()
        {
            _playerInformationProvider.Dashed -= PlayDashEffects;
        }

        private void Start()
        {
            _dashEffectsVolume.weight = 0;
            _dashEffectsVolume.enabled = false;
        }

        private void PlayDashEffects()
        {
            _dashEffectsVolume.enabled = true;
            DOVirtual
                .Float(0, 1, _applyDashEffectsVolumeDurationSeconds,
                    currentWeight => _dashEffectsVolume.weight = currentWeight).SetLink(gameObject)
                .SetEase(_applyDashEffectsVolumeEase)
                .OnComplete(() =>
                    DOVirtual.Float(1, 0, _applyDashEffectsVolumeDurationSeconds,
                            currentWeight => _dashEffectsVolume.weight = currentWeight)
                        .SetLink(gameObject)
                        .SetEase(_applyDashEffectsVolumeEase).OnComplete(() => _dashEffectsVolume.enabled = false));
        }
    }
}