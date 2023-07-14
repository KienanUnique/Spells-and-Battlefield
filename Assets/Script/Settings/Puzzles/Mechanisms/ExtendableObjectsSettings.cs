using DG.Tweening;
using UnityEngine;

namespace Settings.Puzzles.Mechanisms
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PuzzleMechanismsDirectory + "Extendable Object Settings",
        fileName = "Extendable Objects Settings", order = 0)]
    public class ExtendableObjectsSettings : ScriptableObject
    {
        [SerializeField] private Ease _animationEase = Ease.OutSine;
        public Ease AnimationEase => _animationEase;
    }
}