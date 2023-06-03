using System;
using UnityEngine;

namespace Pickable_Items.Card_Information
{
    [Serializable]
    public class CardInformation : ICardInformation
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;
        public Sprite Icon => _icon;
        public string Title => _title;
    }
}