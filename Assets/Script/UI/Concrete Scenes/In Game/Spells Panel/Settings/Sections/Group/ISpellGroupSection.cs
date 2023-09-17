using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group
{
    public interface ISpellGroupSection
    {
        float SelectionAnimationDuration { get; }
        Ease SelectionAnimationEase { get; }
        float SelectedGroupScaleCoefficient { get; }
        float UnselectedGroupScaleCoefficient { get; }
        Vector3 EmptyAnimationPunchStrength { get; }
        float EmptyAnimationDuration { get; }
        int EmptyAnimationPunchVibratoCount { get; }
        float EmptyAnimationPunchElasticity { get; }
    }
}