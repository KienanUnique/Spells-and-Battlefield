using System;
using System.Collections;
using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Continuous_Effect.Information_For_Creation;
using Common.Mechanic_Effects.Source;
using DG.Tweening;
using UnityEngine;

namespace Common.Mechanic_Effects.Continuous_Effect
{
    public class ContinuousEffect : IContinuousEffect
    {
        private readonly float _cooldownInSeconds;
        private readonly List<IMechanicEffect> _mechanics;
        private readonly bool _needIgnoreCooldown;
        private readonly IInteractable _target;
        private readonly IEffectSourceInformation _effectSourceInformation;
        private ICoroutineStarter _coroutineStarter;
        private float _passedSeconds;
        private Coroutine _effectCooldownCoroutine;
        private Tween _tween;

        public ContinuousEffect(IContinuousEffectInformationForCreation continuousEffectInformationForCreation,
            IInteractable target)
        {
            _cooldownInSeconds = continuousEffectInformationForCreation.CooldownInSeconds;
            DurationInSeconds = continuousEffectInformationForCreation.DurationInSeconds;
            _needIgnoreCooldown = continuousEffectInformationForCreation.NeedIgnoreCooldown;
            _mechanics = continuousEffectInformationForCreation.Mechanics;
            Icon = continuousEffectInformationForCreation.Icon;
            _target = target;
            _effectSourceInformation = new EffectSourceInformation(EffectSourceType.Local, _target.MainTransform);
        }

        public event Action<IContinuousEffect> EffectEnded;
        public event Action<float> RatioOfCompletedPartToEntireDurationChanged;

        public Sprite Icon { get; }

        public float CurrentRatioOfCompletedPartToEntireDuration => _passedSeconds / DurationInSeconds;
        public float DurationInSeconds { get; }

        public void Start(ICoroutineStarter coroutineStarter, GameObject gameObjectToLink)
        {
            if (_coroutineStarter != null || _effectCooldownCoroutine != null || _tween != null)
            {
                throw new InvalidOperationException("ContinuousEffect Start method was called more than once");
            }

            _coroutineStarter = coroutineStarter;
            _passedSeconds = 0f;
            _tween = DOTween
                     .To(() => _passedSeconds, newPassedSeconds => _passedSeconds = newPassedSeconds, DurationInSeconds,
                         DurationInSeconds)
                     .OnUpdate(() =>
                     {
                         RatioOfCompletedPartToEntireDurationChanged?.Invoke(
                             CurrentRatioOfCompletedPartToEntireDuration);
                     })
                     .SetLink(gameObjectToLink)
                     .OnComplete(End);
            if (_needIgnoreCooldown)
            {
                ApplyEffects();
            }
            else
            {
                _effectCooldownCoroutine = coroutineStarter.StartCoroutine(ApplyEffectWithCooldown());
            }
        }

        public void End()
        {
            _tween.Kill();
            _tween = null;
            if (_effectCooldownCoroutine != null)
            {
                _coroutineStarter.StopCoroutine(_effectCooldownCoroutine);
                _effectCooldownCoroutine = null;
            }

            _coroutineStarter = null;

            foreach (IMechanicEffect mechanic in _mechanics)
            {
                if (mechanic is IMechanicEffectWithRollback mechanicEffectWithRollback)
                {
                    mechanicEffectWithRollback.Rollback();
                }
            }

            EffectEnded?.Invoke(this);
        }

        private void ApplyEffects()
        {
            _mechanics.ForEach(effect => effect.ApplyEffectToTarget(_target, _effectSourceInformation));
        }

        private IEnumerator ApplyEffectWithCooldown()
        {
            var waitForCooldown = new WaitForSeconds(_cooldownInSeconds);
            var passedSeconds = 0f;
            while (passedSeconds < DurationInSeconds)
            {
                ApplyEffects();
                yield return waitForCooldown;
                passedSeconds += _cooldownInSeconds;
            }

            End();
        }
    }
}