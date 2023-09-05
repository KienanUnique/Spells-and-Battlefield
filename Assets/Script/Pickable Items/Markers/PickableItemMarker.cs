using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Common.Editor_Label_Text_Display;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using UnityEngine;

namespace Pickable_Items.Markers
{
    [RequireComponent(typeof(EditorLabelTextDisplay))]
    public class PickableItemMarker : SpawnMarkerBase, IPickableItemMarker, ITextForEditorLabelProvider
    {
        [SerializeField] private PickableItemScriptableObjectBase _objectToSpawn;

        public IPickableItemDataForCreating DataForCreating => _objectToSpawn;
        public string TextForEditorLabel => _objectToSpawn == null ? string.Empty : _objectToSpawn.name;
    }
}