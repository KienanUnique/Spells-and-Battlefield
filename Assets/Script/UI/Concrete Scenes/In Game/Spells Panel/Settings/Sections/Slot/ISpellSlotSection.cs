using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Slot
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