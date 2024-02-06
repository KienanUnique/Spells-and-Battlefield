using System;

namespace Player
{
    public interface IPlayerOverSpeedInformation
    {
        public event Action<float> OverSpeedValueChanged;
        public float CurrentOverSpeedRatio { get; }
    }
}