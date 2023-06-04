using TMPro;
using UnityEngine.UI;

namespace Pickable_Items.Card_Controls
{
    public class CardControls : ICardControls
    {
        public CardControls(TMP_Text title, Image icon)
        {
            Title = title;
            Icon = icon;
        }

        public TMP_Text Title { get; }
        public Image Icon { get; }
    }
}