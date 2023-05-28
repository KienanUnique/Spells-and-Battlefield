using System;
using Common.Abstract_Bases;
using Interfaces.Pickers;

namespace Pickable_Items
{
    public class DroppedItemsPickerTrigger : BoxColliderTriggerBase<IDroppedItemsPicker>
    {
        public event Action<IDroppedItemsPicker> PickerDetected;

        private void OnEnable()
        {
            RequiredObjectEnteringDetected += OnRequiredObjectEnteringDetected;
        }

        private void OnDisable()
        {
            RequiredObjectEnteringDetected -= OnRequiredObjectEnteringDetected;
        }

        private void OnRequiredObjectEnteringDetected(IDroppedItemsPicker obj)
        {
            PickerDetected?.Invoke(obj);
        }
    }
}