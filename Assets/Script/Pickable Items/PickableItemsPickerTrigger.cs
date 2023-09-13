using System;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Interfaces.Pickers;

namespace Pickable_Items
{
    public class PickableItemsPickerTrigger : TriggerForInitializableObjectsBase<IPickableItemsPicker>
    {
        public event Action<IPickableItemsPicker> PickerDetected;

        protected override void OnEnable()
        {
            base.OnEnable();
            RequiredObjectEnteringDetected += OnPickerEnteringDetected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RequiredObjectEnteringDetected -= OnPickerEnteringDetected;
        }

        private void OnPickerEnteringDetected(IPickableItemsPicker picker)
        {
            PickerDetected?.Invoke(picker);
        }
    }
}