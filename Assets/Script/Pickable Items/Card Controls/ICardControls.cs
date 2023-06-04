using TMPro;
using UnityEngine.UI;

namespace Pickable_Items.Card_Controls
{
    public interface ICardControls
    {
        public TMP_Text Title { get; }
        public Image Icon { get; }
    }
}