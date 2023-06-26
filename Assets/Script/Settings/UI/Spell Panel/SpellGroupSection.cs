using System;
using DG.Tweening;
using UnityEngine;

namespace Settings.UI.Spell_Panel
{
    [Serializable]
    public class SpellGroupSection
    {
        [SerializeField] private float _selectedGroupScaleCoefficient = 1.5f;
        [SerializeField] private Ease _selectionAnimationEase = Ease.OutQuint;
        [SerializeField] private float _selectionAnimationDuration = 0.3f;

        public float SelectionAnimationDuration => _selectionAnimationDuration;
        public Ease SelectionAnimationEase => _selectionAnimationEase;
        public float SelectedGroupScaleCoefficient => _selectedGroupScaleCoefficient;
        public float UnselectedGroupScaleCoefficient => 1;
    }
}