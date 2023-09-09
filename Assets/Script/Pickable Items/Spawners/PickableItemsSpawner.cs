using Common.Abstract_Bases.Spawn_Markers_System.Spawners;
using Pickable_Items.Factory;
using Pickable_Items.Markers;
using Zenject;

namespace Pickable_Items.Spawners
{
    public class PickableItemsSpawner : SpawnerBase<IPickableItemMarker>
    {
        private IPickableItemsFactory _pickableItemsFactory;

        [Inject]
        private void GetDependencies(IPickableItemsFactory pickableItemsFactory)
        {
            _pickableItemsFactory = pickableItemsFactory;
        }

        protected override void Spawn()
        {
            foreach (IPickableItemMarker marker in _markers)
            {
                _pickableItemsFactory.Create(marker.DataForCreating, marker.SpawnPosition);
            }
        }
    }
}