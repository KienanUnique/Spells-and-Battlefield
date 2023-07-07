using DG.Tweening;
using UnityEngine;

namespace Settings.Puzzles.Mechanisms
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PuzzleMechanismsDirectory + "Moving Platform With Stops Settings",
        fileName = "Moving Platform With Stops Settings", order = 0)]
    public class MovingPlatformWithStopsSettings : ScriptableObject
    {
        [SerializeField] private Ease _movementEase;
        public Ease MovementEase => _movementEase;
    }
}