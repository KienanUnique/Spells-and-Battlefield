using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group
{
    [Serializable]
    public class SpellGroupSection : ISpellGroupSection
    {
        [Header("Selection Animation")]
        [SerializeField]
        private float _selectedGroupScaleCoefficient = 1.5f;

        [SerializeField] private Ease _selectionAnimationEase = Ease.OutQuint;
        [SerializeField] private float _selectionAnimationDuration = 0.3f;

        [Header("Empty Animation")]
        [SerializeField]
        private Vector3 _emptyAnimationPunchStrength;

        [SerializeField] private float _emptyAnimationDuration = 0.3f;
        [SerializeField] private int _emptyAnimationPunchVibratoCount = 3;
        [SerializeField] private float _emptyAnimationPunchElasticity = 0.3f;

        public float SelectionAnimationDuration => _selectionAnimationDuration;
        public Ease SelectionAnimationEase => _selectionAnimationEase;
        public float SelectedGroupScaleCoefficient => _selectedGroupScaleCoefficient;
        public float UnselectedGroupScaleCoefficient => 1;
        public Vector3 EmptyAnimationPunchStrength => _emptyAnimationPunchStrength;
        public float EmptyAnimationDuration => _emptyAnimationDuration;
        public int EmptyAnimationPunchVibratoCount => _emptyAnimationPunchVibratoCount;
        public float EmptyAnimationPunchElasticity => _emptyAnimationPunchElasticity;
    }
}