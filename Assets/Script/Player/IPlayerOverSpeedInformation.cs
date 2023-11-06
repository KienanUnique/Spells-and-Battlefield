using System;

namespace Player
{
    public interface IPlayerOverSpeedInformation
    {
        public float CurrentOverSpeedRatio { get; }
        public event Action<float> OverSpeedValueChanged;
    }
}