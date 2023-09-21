using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Common.Editor_Label_Text_Display;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Pickable_Items.Factory;
using UnityEngine;
using Zenject;

namespace Pickable_Items.Spawner
{
    [RequireComponent(typeof(EditorLabelTextDisplay))]
    public class PickableItemSpawner : SpawnMarkerBase, ITextForEditorLabelProvider
    {
        [SerializeField] private PickableItemScriptableObjectBase _objectToSpawn;
        private IPickableItemsFactory _pickableItemsFactory;

        [Inject]
        private void GetDependencies(IPickableItemsFactory pickableItemsFactory)
        {
            _pickableItemsFactory = pickableItemsFactory;
        }

        public string TextForEditorLabel => _objectToSpawn == null ? string.Empty : _objectToSpawn.name;

        private void Start()
        {
            _pickableItemsFactory.Create(_objectToSpawn, transform.position);
        }
    }
}