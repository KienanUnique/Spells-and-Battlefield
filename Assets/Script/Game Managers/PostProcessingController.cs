using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Game_Managers
{
    public class PostProcessingController : Singleton<PostProcessingController>
    {
        [SerializeField] private Volume _dashEffectsVolume;
        [SerializeField] private float _applyDashEffectsVolumeDurationSeconds = 0.2f;
        [SerializeField] private Ease _applyDashEffectsVolumeEase = Ease.OutCubic;

        private IPlayerInformation _playerInformation;

        [Inject]
        private void Construct(IPlayerInformation playerInformation)
        {
            _playerInformation = playerInformation;
        }

        private void OnEnable()
        {
            _playerInformation.Dashed += PlayDashEffects;
        }

        private void OnDisable()
        {
            _playerInformation.Dashed -= PlayDashEffects;
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