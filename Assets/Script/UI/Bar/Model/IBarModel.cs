using System;

namespace UI.Bar.Model
{
    public interface IBarModel
    {
        public event Action<float> FillAmountChanged;
        public float CurrentFillAmount { get; }
    }
}