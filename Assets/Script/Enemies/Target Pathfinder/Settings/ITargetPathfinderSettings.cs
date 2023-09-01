namespace Enemies.Target_Pathfinder.Settings
{
    public interface ITargetPathfinderSettings
    {
        public float UpdateDestinationCooldownSeconds { get; }
        public float NextWaypointDistance { get; }
        public float MaxDistanceFromTargetToNavMesh { get; }
    }
}