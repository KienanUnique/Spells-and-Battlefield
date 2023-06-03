using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Pickable_Items.Data_For_Creating;

namespace Pickable_Items.Markers
{
    public interface IPickableItemMarker : ISpawnMarker
    {
        public IPickableItemDataForCreating DataForCreating { get; }
    }
}