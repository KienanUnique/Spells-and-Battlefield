using DG.Tweening;
using UnityEngine;

namespace Common.Dissolve_Effect_Controller.Settings
{
    [CreateAssetMenu(fileName = "Dissolve Effect Controller Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Dissolve Effect Controller Settings",
        order = 0)]
    public class DissolveEffectControllerSettings : ScriptableObject, IDissolveEffectControllerSettings
    {
        [SerializeField] private float _dissolveAnimationDuration = 2f;
        [SerializeField] private Ease _dissolveAnimationEase = Ease.OutCubic;

        public float DissolveAnimationDuration => _dissolveAnimationDuration;
        public Ease DissolveAnimationEase => _dissolveAnimationEase;
    }
}