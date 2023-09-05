using DG.Tweening;
using Puzzles.Mechanisms.Moving_Platforms.Settings;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public static class MovingPlatformTweenCustomExtension
    {
        public static T ApplyCustomSetupForMovingPlatforms<T>(this T t, GameObject gameObjectToLink,
            IMovingPlatformsSettings settings, float delayInSeconds) where T : Tween
        {
            var tween = t as Tween;
            tween.SetSpeedBased().SetEase(settings.MovementEase).SetLink(gameObjectToLink).SetDelay(delayInSeconds);
            return t;
        }
    }
}