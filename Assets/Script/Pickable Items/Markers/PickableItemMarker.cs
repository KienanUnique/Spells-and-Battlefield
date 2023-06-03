using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using UnityEngine;

namespace Pickable_Items.Markers
{
    public class PickableItemMarker : SpawnMarkerBase, IPickableItemMarker
    {
        [SerializeField] private PickableItemScriptableObjectBase _objectToSpawn;

        public IPickableItemDataForCreating DataForCreating => _objectToSpawn;
    }
}