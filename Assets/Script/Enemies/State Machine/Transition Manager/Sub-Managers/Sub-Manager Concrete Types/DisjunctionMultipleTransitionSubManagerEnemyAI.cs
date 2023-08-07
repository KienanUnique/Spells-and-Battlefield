using System;
using System.Linq;

namespace Enemies.State_Machine.Transition_Manager.Sub_Managers.Sub_Manager_Concrete_Types
{
    [Serializable]
    public class DisjunctionMultipleTransitionSubManagerEnemyAI : MultipleTransitionSubManagerEnemyAIBase
    {
        protected override bool IsNeedConditionsCompleted()
        {
            return _conditions.Any(condition => condition.IsConditionCompleted);
        }
    }
}