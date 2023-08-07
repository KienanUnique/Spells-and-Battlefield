using System;
using System.Linq;

namespace Enemies.State_Machine.Transition_Manager.Sub_Managers.Sub_Manager_Concrete_Types
{
    [Serializable]
    public class ConjunctionMultipleTransitionSubManagerEnemyAI : MultipleTransitionSubManagerEnemyAIBase
    {
        protected override bool IsNeedConditionsCompleted()
        {
            return _conditions.All(condition => condition.IsConditionCompleted);
        }
    }
}