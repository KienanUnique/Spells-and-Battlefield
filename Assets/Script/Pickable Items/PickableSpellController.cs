﻿using Interfaces;
using Interfaces.Pickers;
using Spells;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Pickable_Items
{
    public class PickableSpellController : PickableItemBase<ISpell>
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private SpellBase _startStoredSpell;

        protected override void Initialize()
        {
            if (_startStoredSpell != null)
            {
                StoredObject = _startStoredSpell;
            }
        }

        protected override void SpecialAppearAction()
        {
            _title.SetText(StoredObject.Title);
            _icon.sprite = StoredObject.Icon;
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