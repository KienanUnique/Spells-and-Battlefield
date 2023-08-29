using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;

namespace Common.Abstract_Bases.Spawn_Markers_System.Markers
{
    public interface ISpawnMarker : IPositionDataForInstantiation
    {
        public bool IsDisabled { get; }
    }
}