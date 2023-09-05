using DG.Tweening;
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
    }
}