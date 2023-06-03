using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Character;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Trigger;
using General_Settings_in_Scriptable_Objects;
using Interfaces;
using Pathfinding;
using Pickable_Items;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Pickable_Items.Factory;
using Settings;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Enemies.Setup
{
    public abstract class EnemyControllerSetupBase<TController> : MonoBehaviour, ICoroutineStarter,
        IEnemyTriggersSettable
    {
        [SerializeField] protected EnemyStateMachineAI _enemyStateMachineAI;
        [SerializeField] protected PickableItemScriptableObjectBase _itemToDrop;
        [SerializeField] private List<EnemyTargetTrigger> _targetTriggers;
        private EnemyMovement _enemyMovement;
        private List<IDisableable> _itemsNeedDisabling;
        private IdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableItemsFactory _itemsFactory;
        private EnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        protected abstract IEnemySettings EnemySettings { get; }
        protected abstract CharacterBase Character { get; }

        [Inject]
        private void Construct(GeneralEnemySettings generalEnemySettings, IPickableItemsFactory itemsFactory)
        {
            _generalEnemySettings = generalEnemySettings;
            _itemsFactory = itemsFactory;
        }

        public void SetExternalEnemyTargetTriggers(List<Trigger.IEnemyTargetTrigger> enemyTargetTriggers)
        {
            enemyTargetTriggers.ForEach(trigger => _targetFromTriggersSelector.AddTrigger(trigger));
        }

        protected abstract void SetupConcreteController(IEnemyBaseSetupData baseSetupData,
            TController controllerToSetup);

        protected abstract void SpecialAwakeAction();

        private void Awake()
        {
            SpecialAwakeAction();
            _idHolder = GetComponent<IdHolder>();
            var seeker = GetComponent<Seeker>();
            var thisRigidbody = GetComponent<Rigidbody>();
            _enemyMovement = new EnemyMovement(this, EnemySettings.MovementSettings,
                EnemySettings.TargetPathfinderSettingsSection, seeker, thisRigidbody);
            _targetFromTriggersSelector = new EnemyTargetFromTriggersSelector();
            _targetTriggers.ForEach(trigger => _targetFromTriggersSelector.AddTrigger(trigger));

            _itemsNeedDisabling = new List<IDisableable>
            {
                _enemyMovement,
                Character,
                _targetFromTriggersSelector
            };
        }

        private void Start()
        {
            var controllerToSetup = GetComponent<TController>();
            var baseSetupData = new EnemyBaseSetupData(
                _enemyStateMachineAI,
                _itemToDrop,
                _enemyMovement,
                _itemsNeedDisabling,
                _idHolder,
                _generalEnemySettings,
                _itemsFactory,
                _targetFromTriggersSelector);

            SetupConcreteController(baseSetupData, controllerToSetup);
        }
    }
}