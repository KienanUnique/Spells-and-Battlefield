using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Editor_Label_Text_Display;
using Common.Spawn;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Pickable_Items.Factory;
using UnityEngine;
using Zenject;

namespace Pickable_Items.Spawner
{
    [RequireComponent(typeof(EditorLabelTextDisplay))]
    public class PickableItemSpawner : MonoBehaviour, ISpawner, ITextForEditorLabelProvider
    {
        [SerializeField] private PickableItemScriptableObjectBase _objectToSpawn;
        private IPickableItemsFactory _pickableItemsFactory;

        [Inject]
        private void GetDependencies(IPickableItemsFactory pickableItemsFactory)
        {
            _pickableItemsFactory = pickableItemsFactory;
        }

        public event Action<InitializableMonoBehaviourStatus> InitializationStatusChanged;

        public InitializableMonoBehaviourStatus CurrentInitializableMonoBehaviourStatus =>
            InitializableMonoBehaviourStatus.Initialized;

        public string TextForEditorLabel => _objectToSpawn == null ? string.Empty : _objectToSpawn.name;

        public void Spawn()
        {
            _pickableItemsFactory.Create(_objectToSpawn, transform.position);
        }

        private void Awake()
        {
            InitializationStatusChanged?.Invoke(CurrentInitializableMonoBehaviourStatus);
        }
    }
}