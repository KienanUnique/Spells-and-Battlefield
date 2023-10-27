using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Settings
{
    [CreateAssetMenu(fileName = "Comics Panel Settings",
        menuName = ScriptableObjectsMenuDirectories.ComicsSettingsDirectory + "Comics Panel Settings", order = 0)]
    public class ComicsPanelSettings : ScriptableObject, IComicsPanelSettings
    {
        [SerializeField] private float _appearAnimationDurationInSeconds = 1f;
        [SerializeField] private float _disappearAnimationDurationInSeconds = 0.5f;
        [SerializeField] private float _appearOffsetFromFinalPosition = 10f;
        [SerializeField] private Ease _appearMoveAnimationEase = Ease.OutCubic;
        [SerializeField] private float _panelDisplayTimeInSeconds = 3f;

        public float AppearAnimationDurationInSeconds => _appearAnimationDurationInSeconds;
        public float DisappearAnimationDurationInSeconds => _disappearAnimationDurationInSeconds;
        public float AppearOffsetFromFinalPosition => _appearOffsetFromFinalPosition;
        public Ease AppearMoveAnimationEase => _appearMoveAnimationEase;
        public float PanelDisplayTimeInSeconds => _panelDisplayTimeInSeconds;
    }
}