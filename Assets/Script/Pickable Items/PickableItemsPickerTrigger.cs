﻿using System;
using Common.Abstract_Bases;
using Interfaces.Pickers;

namespace Pickable_Items
{
    public class PickableItemsPickerTrigger : BoxColliderTriggerBase<IPickableItemsPicker>
    {
        public event Action<IPickableItemsPicker> PickerDetected;

        private void OnEnable()
        {
            RequiredObjectEnteringDetected += OnRequiredObjectEnteringDetected;
        }

        private void OnDisable()
        {
            RequiredObjectEnteringDetected -= OnRequiredObjectEnteringDetected;
        }

        private void OnRequiredObjectEnteringDetected(IPickableItemsPicker obj)
        {
            PickerDetected?.Invoke(obj);
        }
    }
}