using System;

namespace UI.Concrete_Scenes.In_Game.Bar.Model
{
    public interface IBarModel
    {
        public event Action<float> FillAmountChanged;
        public float CurrentFillAmount { get; }
    }
}