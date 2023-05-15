using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Continuous_Effect
{
    public class ContinuousEffect : IContinuousEffect
    {
        private readonly float _cooldownInSeconds;
        private readonly float _durationInSeconds;
        private readonly List<ISpellMechanicEffect> _mechanics;
        private readonly bool _needIgnoreCooldown;
        private Coroutine _effectCoroutine;
        private ISpellInteractable _target;
        private MonoBehaviour _fromStartedMonoBehaviour;

        public ContinuousEffect(float cooldownInSeconds, List<ISpellMechanicEffect> mechanics, float durationInSeconds,
            bool needIgnoreCooldown)
        {
            _cooldownInSeconds = cooldownInSeconds;
            _durationInSeconds = durationInSeconds;
            _needIgnoreCooldown = needIgnoreCooldown;
            _mechanics = mechanics;
        }

        public event Action<IContinuousEffect> EffectEnded;

        public void Start(MonoBehaviour target)
        {
            if (_effectCoroutine == null && target.TryGetComponent<ISpellInteractable>(out var targetSpellInteractable))
            {
                _fromStartedMonoBehaviour = target;
                _target = targetSpellInteractable;
                _effectCoroutine =
                    target.StartCoroutine(_needIgnoreCooldown ? WaitTillEnd() : ApplyEffectWithCooldown());
            }
        }

        public void End()
        {
            if (_effectCoroutine == null) return;
            _fromStartedMonoBehaviour.StopCoroutine(_effectCoroutine);
            _fromStartedMonoBehaviour = null;
            _effectCoroutine = null;
            foreach (var mechanic in _mechanics)
            {
                if (mechanic is ISpellMechanicEffectWithRollback mechanicEffectWithRollback)
                {
                    mechanicEffectWithRollback.Rollback();
                }
            }

            EffectEnded?.Invoke(this);
        }

        private void ApplyEffects()
        {
            _mechanics.ForEach(effect => effect.ApplyEffectToTarget(_target));
        }

        private IEnumerator ApplyEffectWithCooldown()
        {
            var passedSeconds = 0f;
            while (passedSeconds < _durationInSeconds)
            {
                ApplyEffects();
                yield return new WaitForSeconds(_cooldownInSeconds);
                passedSeconds += _cooldownInSeconds;
            }

            End();
        }

        private IEnumerator WaitTillEnd()
        {
            ApplyEffects();
            yield return new WaitForSeconds(_durationInSeconds);
            End();
        }
    }
}