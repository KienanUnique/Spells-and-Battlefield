using System.Collections.Generic;
using Common.Dissolve_Effect_Controller.Settings;
using DG.Tweening;
using UnityEngine;

namespace Common.Dissolve_Effect_Controller
{
    public class DissolveEffectController : IDissolveEffectController
    {
        private const float NoneDissolveEffectValue = 0f;
        private const float FullDissolveEffectValue = 1f;
        private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");
        private readonly IDissolveEffectControllerSettings _settings;
        private readonly ICollection<Material> _materials;
        private readonly GameObject _linkGameObject;

        public DissolveEffectController(IEnumerable<Renderer> renderers, IDissolveEffectControllerSettings settings,
            GameObject linkGameObject, bool isDissolvedAtStart = false)
        {
            _settings = settings;
            _linkGameObject = linkGameObject;
            var materials = new List<Material>();
            foreach (Renderer renderer in renderers)
            {
                materials.AddRange(renderer.materials);
            }

            _materials = materials;

            var startDissolveValue = isDissolvedAtStart ? FullDissolveEffectValue : NoneDissolveEffectValue;
            foreach (Material material in _materials)
            {
                material.SetFloat(Dissolve, startDissolveValue);
            }
        }

        public void Appear()
        {
            ChangeDissolveValue(FullDissolveEffectValue);
            ChangeDissolveValueSmoothly(NoneDissolveEffectValue);
        }

        public void Disappear()
        {
            ChangeDissolveValue(NoneDissolveEffectValue);
            ChangeDissolveValueSmoothly(FullDissolveEffectValue);
        }

        private void ChangeDissolveValueSmoothly(float endValue)
        {
            Sequence dissolveSequence = DOTween.Sequence();
            dissolveSequence.SetLink(_linkGameObject);

            foreach (Material material in _materials)
            {
                dissolveSequence.Join(material.DOFloat(endValue, Dissolve, _settings.DissolveAnimationDuration)
                                              .SetEase(_settings.DissolveAnimationEase));
            }
        }

        private void ChangeDissolveValue(float endValue)
        {
            foreach (Material material in _materials)
            {
                material.SetFloat(Dissolve, endValue);
            }
        }
    }
}