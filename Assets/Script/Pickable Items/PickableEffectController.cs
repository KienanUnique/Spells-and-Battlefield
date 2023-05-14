using Interfaces.Pickers;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Pickable_Items
{
    public class PickableEffectController : PickableItemBase<ISpellMechanicEffect>
    {
        [SerializeField] private SpellMechanicEffectScriptableObject _effect;

        protected override void SpecialPickUpAction(IDroppedItemsPicker picker)
        {
            if (picker is IDroppedEffectPicker droppedEffectPicker)
            {
                StoredObject.ApplyEffectToTarget(droppedEffectPicker);
            }
        }

        protected override void SpecialAppearAction()
        {
        }

        protected override bool CanBePickedUpByThisPeeker(IDroppedItemsPicker picker)
        {
            return picker is IDroppedEffectPicker;
        }

        protected override void Initialize()
        {
            StoredObject = _effect.GetImplementationObject();
        }
    }
}