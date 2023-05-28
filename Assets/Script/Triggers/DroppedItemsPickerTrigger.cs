using System;
using Interfaces.Pickers;

namespace Triggers
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