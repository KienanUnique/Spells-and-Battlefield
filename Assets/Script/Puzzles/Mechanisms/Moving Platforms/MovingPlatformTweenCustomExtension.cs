using DG.Tweening;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public static class MovingPlatformTweenCustomExtension
    {
        public static T ApplyCustomSetupForMovingPlatforms<T>(this T t, GameObject gameObjectToLink,
            MovingPlatformsSettings settings, float delayInSeconds) where T : Tween
        {
            var tween = t as Tween;
            tween.SetSpeedBased().SetEase(settings.MovementEase).SetLink(gameObjectToLink).SetDelay(delayInSeconds);
            return t;
        }
    }
}