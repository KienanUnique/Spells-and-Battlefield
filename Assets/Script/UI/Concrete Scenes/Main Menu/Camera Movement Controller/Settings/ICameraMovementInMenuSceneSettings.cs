using DG.Tweening;

namespace UI.Concrete_Scenes.Main_Menu.Camera_Movement_Controller.Settings
{
    public interface ICameraMovementInMenuSceneSettings
    {
        public float MoveAnimationDurationInSeconds { get; }
        public PathType MovePathType { get; }
        public Ease MoveEase { get; }
        public float PartOfTimelineToStartRotateCameraToOldRotation { get; }
        public Ease RotateCameraEase { get; }
    }
}