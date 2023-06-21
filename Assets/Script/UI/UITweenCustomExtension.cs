using DG.Tweening;
using General_Settings_in_Scriptable_Objects;
using Settings;
using Settings.UI;
using UnityEngine;

namespace UI
{
    public static class UITweenCustomExtension
    {
        public static T ApplyCustomSetupForUI<T>(this T t, GameObject gameObjectToLink) where T : Tween
        {
            var tween = t as Tween;
            tween.SetLink(gameObjectToLink).SetUpdate(true);
            return t;
        }

        public static Tween SetupAppearAnimationForUI<T>(this T t, GeneralUIAnimationSettings settings,
            GameObject gameObjectToLink) where T : Transform
        {
            var transform = t as Transform;
            return transform.ApplyScaleAnimationForUI(settings, gameObjectToLink, Vector3.one);
        }

        public static Tween SetupDisappearAnimationForUI<T>(this T t, GeneralUIAnimationSettings settings,
            GameObject gameObjectToLink) where T : Transform
        {
            var transform = t as Transform;
            return transform.ApplyScaleAnimationForUI(settings, gameObjectToLink, Vector3.zero);
        }

        private static Tween ApplyScaleAnimationForUI<T>(this T t, GeneralUIAnimationSettings settings,
            GameObject gameObjectToLink, Vector3 finalScaleValue)
            where T : Transform
        {
            var transform = t as Transform;
            return transform.DOScale(finalScaleValue, settings.ScaleAnimationDuration)
                .ApplyCustomSetupForUI(gameObjectToLink).SetEase(settings.ScaleAnimationEase);
        }
    }
}