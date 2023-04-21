using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells
{
    public class ContinuousEffect : IContinuousEffect
    {
        private readonly float _cooldownInSeconds;
        private readonly float _durationInSeconds;
        private readonly List<ISpellMechanicEffect> _effects;
        private Coroutine _effectCoroutine = null;
        private ISpellInteractable _target;
        private MonoBehaviour _fromStartedMonoBehaviour;

        public ContinuousEffect(float cooldownInSeconds, List<ISpellMechanicEffect> effects, float durationInSeconds)
        {
            _cooldownInSeconds = cooldownInSeconds;
            _durationInSeconds = durationInSeconds;
            _effects = effects;
        }

        public event Action<IContinuousEffect> EffectEnded;

        public void Start(MonoBehaviour target)
        {
            if (_effectCoroutine == null && target.TryGetComponent<ISpellInteractable>(out var targetSpellInteractable))
            {
                _fromStartedMonoBehaviour = target;
                _target = targetSpellInteractable;
                _effectCoroutine = target.StartCoroutine(CooldownEffect());
            }
        }

        public void End()
        {
            if (_effectCoroutine == null) return;
            _fromStartedMonoBehaviour.StopCoroutine(_effectCoroutine);
            _fromStartedMonoBehaviour = null;
            _effectCoroutine = null;
            EffectEnded?.Invoke(this);
        }

        private IEnumerator CooldownEffect()
        {
            var passedSeconds = 0f;
            while (passedSeconds < _durationInSeconds)
            {
                _effects.ForEach(effect => effect.ApplyEffectToTarget(_target));
                yield return new WaitForSeconds(_cooldownInSeconds);
                passedSeconds += _cooldownInSeconds;
            }

            End();
        }
    }
}