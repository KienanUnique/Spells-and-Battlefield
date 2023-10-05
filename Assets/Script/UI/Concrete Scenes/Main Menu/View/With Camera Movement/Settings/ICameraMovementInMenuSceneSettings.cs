using DG.Tweening;

namespace UI.Concrete_Scenes.Main_Menu.View.With_Camera_Movement.Settings
{
    public interface ICameraMovementInMenuSceneSettings
    {
        public float MoveAnimationDurationInSeconds { get; }
        public PathType MovePathType { get; }
        public Ease MoveEase { get; }
        public float FinalRotateCameraBeforeEndOfTheMovementDurationInSeconds { get; }
        public Ease FinalRotateCameraEase { get; }
    }
}