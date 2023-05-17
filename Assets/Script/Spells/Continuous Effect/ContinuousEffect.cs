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
        private ICoroutineStarter _coroutineStarter;

        public ContinuousEffect(float cooldownInSeconds, List<ISpellMechanicEffect> mechanics, float durationInSeconds,
            bool needIgnoreCooldown)
        {
            _cooldownInSeconds = cooldownInSeconds;
            _durationInSeconds = durationInSeconds;
            _needIgnoreCooldown = needIgnoreCooldown;
            _mechanics = mechanics;
        }

        public event Action<IContinuousEffect> EffectEnded;

        public void Start(ICoroutineStarter coroutineStarter)
        {
            if (_effectCoroutine == null)
            {
                _coroutineStarter = coroutineStarter;
                _effectCoroutine =
                    coroutineStarter.StartCoroutine(_needIgnoreCooldown ? WaitTillEnd() : ApplyEffectWithCooldown());
            }
        }

        public void End()
        {
            if (_effectCoroutine == null) return;
            _coroutineStarter.StopCoroutine(_effectCoroutine);
            _coroutineStarter = null;
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

        public void SetTarget(ISpellInteractable target)
        {
            _target = target;
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