using Common.Abstract_Bases.Spawn_Markers_System.Spawners;
using Pickable_Items.Factory;
using Pickable_Items.Markers;
using Zenject;

namespace Pickable_Items.Spawners
{
    public class PickableItemsSpawner : SpawnerBase<IPickableItemMarker>
    {
        private const bool NeedCreatedItemsFallDown = false;
        private IPickableItemsFactory _pickableItemsFactory;

        [Inject]
        private void Construct(IPickableItemsFactory pickableItemsFactory)
        {
            _pickableItemsFactory = pickableItemsFactory;
        }

        protected override void Spawn()
        {
            foreach (var marker in _markers)
            {
                _pickableItemsFactory.Create(marker.DataForCreating, marker.Position, NeedCreatedItemsFallDown);
            }
        }
    }
}