namespace Enemies.Target_Pathfinder.Settings
{
    public interface ITargetPathfinderSettings
    {
        float UpdateDestinationCooldownSeconds { get; }
        float NextWaypointDistance { get; }
    }
}