using UnityEngine;

namespace Pickable_Items.Card_Information
{
    public interface ICardInformation
    {
        public Sprite Icon { get; }
        public string Title { get; }
    }
}