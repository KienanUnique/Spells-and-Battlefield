using DG.Tweening;
using UnityEngine;

namespace Puzzles.Mechanisms.Extendable_Object.Settings
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PuzzleMechanismsDirectory + "Extendable Object Settings",
        fileName = "Extendable Objects Settings", order = 0)]
    public class ExtendableObjectsSettings : ScriptableObject, IExtendableObjectsSettings
    {
        [SerializeField] private Ease _animationEase = Ease.OutSine;
        public Ease AnimationEase => _animationEase;
    }
}