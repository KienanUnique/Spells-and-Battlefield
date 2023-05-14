using System;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Spell.Implementations
{
    [Serializable]
    public class SpellCardInformation : ISpellCardInformation
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;
        public Sprite Icon => _icon;
        public string Title => _title;
    }
}