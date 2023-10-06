using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller.Settings
{
    [CreateAssetMenu(fileName = "Camera Movement In Menu Scene Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory +
                   "Camera Movement In Menu Scene Settings", order = 0)]
    public class CameraMovementInMenuSceneSettings : ScriptableObject, ICameraMovementInMenuSceneSettings
    {
        [Header("Movement")] [SerializeField] private float _moveAnimationDurationInSeconds = 1f;
        [SerializeField] private PathType _movePathType = PathType.CatmullRom;
        [SerializeField] private Ease _moveEase = Ease.OutCubic;

        [Header("Final Rotation")]
        [Range(0, 1f)]
        [SerializeField]
        private float _partOfTimelineToStartRotateCameraToOldRotation = 0.6f;

        [SerializeField] private Ease _finalRotateCameraEase = Ease.OutCubic;

        public float MoveAnimationDurationInSeconds => _moveAnimationDurationInSeconds;
        public PathType MovePathType => _movePathType;
        public Ease MoveEase => _moveEase;

        public float PartOfTimelineToStartRotateCameraToOldRotation => _partOfTimelineToStartRotateCameraToOldRotation;

        public Ease RotateCameraEase => _finalRotateCameraEase;
    }
}