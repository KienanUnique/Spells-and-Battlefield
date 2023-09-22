using System.Collections.Generic;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.Spawn.Enemy_Target_Triggers_Setter
{
    public class EnemyTargetTriggersSetter : MonoBehaviour
    {
        [SerializeField] private List<EnemyTargetTrigger> _triggersList;

        private void Awake()
        {
            IEnemyTargetTriggersSettable[] targetTriggersSettables =
                GetComponentsInChildren<IEnemyTargetTriggersSettable>();
            foreach (IEnemyTargetTriggersSettable enemyTargetTriggersSettable in targetTriggersSettables)
            {
                enemyTargetTriggersSettable.SetTargetTriggers(new List<IEnemyTargetTrigger>(_triggersList));
            }
        }
    }
}