using DG.Tweening;
using UnityEngine;

namespace Settings.Puzzles.Mechanisms
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PuzzleMechanismsDirectory + "Moving Platforms Settings",
        fileName = "Moving Platforms Settings", order = 0)]
    public class MovingPlatformsSettings : ScriptableObject
    {
        [SerializeField] private Ease _movementEase;
        [SerializeField] private PathType _movementPathType = PathType.CatmullRom;
        public Ease MovementEase => _movementEase;
        public PathType MovementPathType => _movementPathType;
    }
}