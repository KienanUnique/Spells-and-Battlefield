using Enemies.State_Machine.States.Concrete_Types.Use_Spells;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Conditions.Concrete_Types
{
    public class CanUseSpellTransitionConditionEnemyAI : TransitionConditionEnemyAIBase
    {
        [SerializeField] private UseSpellsStateEnemyAI _useSpellStateToCheck;
        public override bool IsConditionCompleted => _useSpellStateToCheck.CanUseSpell;

        protected override void HandleStartCheckingConditions()
        {
            SubscribeOnUseSpellStateEvents();
        }

        protected override void HandleStopCheckingConditions()
        {
            UnsubscribeFromUseSpellStateEvents();
        }

        protected override void SubscribeOnEvents()
        {
            if (!IsEnabled)
            {
                return;
            }

            SubscribeOnUseSpellStateEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromUseSpellStateEvents();
        }

        private void SubscribeOnUseSpellStateEvents()
        {
            _useSpellStateToCheck.CanUseSpellsAgain += OnCanUseSpellsAgain;
        }

        private void UnsubscribeFromUseSpellStateEvents()
        {
            _useSpellStateToCheck.CanUseSpellsAgain -= OnCanUseSpellsAgain;
        }

        private void OnCanUseSpellsAgain()
        {
            InvokeConditionCompletedEvent();
        }
    }
}