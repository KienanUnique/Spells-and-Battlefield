using System;
using Common.Abstract_Bases.Disableable;

namespace UI.Concrete_Scenes.In_Game.Bar.Model
{
    public abstract class BarModelBase : BaseWithDisabling, IBarModel
    {
        public event Action<float> FillAmountChanged;
        public abstract float CurrentFillAmount { get; }

        protected void UpdateFillAmount()
        {
            FillAmountChanged?.Invoke(CurrentFillAmount);
        }
    }
}