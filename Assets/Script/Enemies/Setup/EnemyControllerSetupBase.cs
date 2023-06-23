using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Disableable;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Trigger;
using Interfaces;
using Pathfinding;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Pickable_Items.Factory;
using Settings.Enemy;
using UnityEngine;
using Zenject;

namespace Enemies.Setup
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyControllerSetupBase<TController> : SetupMonoBehaviourBase, ICoroutineStarter,
        IEnemyTriggersSettable
    {
        protected Seeker _seeker;
        protected Rigidbody _thisRigidbody;

        [SerializeField] private EnemyStateMachineAI _enemyStateMachineAI;
        [SerializeField] private PickableItemScriptableObjectBase _itemToDrop;
        [SerializeField] private List<EnemyTargetTrigger> _targetTriggers;
        private List<IDisableable> _itemsNeedDisabling;
        private IdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableItemsFactory _itemsFactory;
        private EnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        protected abstract CharacterBase Character { get; }
        protected abstract EnemyMovement Movement { get; }

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

        protected abstract void SpecialPrepareAction();

        protected override void Prepare()
        {
            _idHolder = GetComponent<IdHolder>();
            _seeker = GetComponent<Seeker>();
            _thisRigidbody = GetComponent<Rigidbody>();
            _targetFromTriggersSelector = new EnemyTargetFromTriggersSelector();
            _targetTriggers.ForEach(trigger => _targetFromTriggersSelector.AddTrigger(trigger));

            SpecialPrepareAction();

            _itemsNeedDisabling = new List<IDisableable>
            {
                Movement,
                Character,
                _targetFromTriggersSelector
            };
        }

        protected override void Initialize()
        {
            var controllerToSetup = GetComponent<TController>();
            var baseSetupData = new EnemyBaseSetupData(
                _enemyStateMachineAI,
                _itemToDrop,
                Movement,
                _itemsNeedDisabling,
                _idHolder,
                _generalEnemySettings,
                _itemsFactory,
                _targetFromTriggersSelector);

            SetupConcreteController(baseSetupData, controllerToSetup);
        }
    }
}