using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Spells_Panel.Settings.Sections.Slot
{
    [Serializable]
    public class SpellSlotSection : ISpellSlotSection
    {
        [SerializeField] private Sprite _emptySlotIcon;

        [Header("Scale animation")]
        [SerializeField]
        private Ease _scaleAnimationEase = Ease.OutQuint;

        [SerializeField] private float _scaleAnimationDuration = 0.3f;

        [Header("Move animation")]
        [SerializeField]
        private Ease _moveAnimationEase = Ease.OutCirc;

        [SerializeField] private float _moveAnimationDuration = 0.3f;
        [SerializeField] private Ease _scaleDuringMovingAnimationEase = Ease.OutQuint;

        public float ScaleAnimationDuration => _scaleAnimationDuration;
        public Ease ScaleAnimationEase => _scaleAnimationEase;
        public Sprite EmptySlotIcon => _emptySlotIcon;
        public Ease MoveAnimationEase => _moveAnimationEase;
        public float MoveAnimationDuration => _moveAnimationDuration;
        public Ease ScaleDuringMovingAnimationEase => _scaleDuringMovingAnimationEase;
    }
}