using Common.Look;

namespace Player.Look.Settings
{
    public interface IPlayerLookSettings : ILookSettings
    {
        public float UpperLimit { get; }
        public float BottomLimit { get; }
    }
}