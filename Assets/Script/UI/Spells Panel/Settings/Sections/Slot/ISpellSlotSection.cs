using DG.Tweening;
using UnityEngine;

namespace UI.Spells_Panel.Settings.Sections.Slot
{
    public interface ISpellSlotSection
    {
        float ScaleAnimationDuration { get; }
        Ease ScaleAnimationEase { get; }
        Sprite EmptySlotIcon { get; }
        Ease MoveAnimationEase { get; }
        float MoveAnimationDuration { get; }
        Ease ScaleDuringMovingAnimationEase { get; }
    }
}