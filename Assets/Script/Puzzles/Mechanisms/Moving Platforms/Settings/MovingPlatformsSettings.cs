using DG.Tweening;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Settings
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PuzzleMechanismsDirectory + "Moving Platforms Settings",
        fileName = "Moving Platforms Settings", order = 0)]
    public class MovingPlatformsSettings : ScriptableObject, IMovingPlatformsSettings
    {
        [SerializeField] private Ease _movementEase = Ease.OutSine;
        [SerializeField] private PathType _movementPathType = PathType.CatmullRom;
        public Ease MovementEase => _movementEase;
        public PathType MovementPathType => _movementPathType;
    }
}