using Interfaces.Pickers;
using Spells;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Pickable_Items
{
    public class PickableSpellController : PickableItemBase<ISpell>
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private SpellScriptableObject _startStoredSpell;

        protected override void Initialize()
        {
            if (_startStoredSpell != null)
            {
                StoredObject = _startStoredSpell.GetImplementationObject();
            }
        }

        protected override void SpecialAppearAction()
        {
            _title.SetText(StoredObject.SpellCardInformation.Title);
            _icon.sprite = StoredObject.SpellCardInformation.Icon;
        }

        protected override bool CanBePickedUpByThisPeeker(IDroppedItemsPicker picker)
        {
            return picker is IDroppedSpellPicker;
        }

        protected override void SpecialPickUpAction(IDroppedItemsPicker picker)
        {
            (picker as IDroppedSpellPicker)?.AddSpell(StoredObject);
        }
    }
}