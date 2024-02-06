using System;
using System.Collections.Generic;
using Enemies.State_Machine.States;
using Enemies.State_Machine.Transition_Manager.Sub_Managers.Sub_Manager_Concrete_Types;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Manager
{
    [Serializable]
    public class MainTransitionManagerEnemyAI : TransitionManagerEnemyAIBase
    {
        [Space]
        [Header("After action state")]
        [SerializeField]
        private bool _needTransitAfterAction;

        [SerializeField] private StateEnemyAI _afterActionNextState;

        [Header("Single")] [SerializeField] private List<SingleTransitionSubManagerEnemyAI> _singleSubManagers;

        [Space]
        [Header("And")]
        [SerializeField]
        private List<ConjunctionMultipleTransitionSubManagerEnemyAI> _conjunctionSubManagers;

        [Space]
        [Header("Or")]
        [SerializeField]
        private List<DisjunctionMultipleTransitionSubManagerEnemyAI> _disjunctionSubManagers;

        public override event Action<IStateEnemyAI> NeedTransit;

        private List<ITransitionManagerEnemyAIWithDisabling> AllSubManagers
        {
            get
            {
                var allSubManagers = new List<ITransitionManagerEnemyAIWithDisabling>();
                allSubManagers.AddRange(_singleSubManagers);
                allSubManagers.AddRange(_conjunctionSubManagers);
                allSubManagers.AddRange(_disjunctionSubManagers);
                return allSubManagers;
            }
        }

        public override bool TryTransit(out IStateEnemyAI stateEnemyAI)
        {
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                if (!subManager.TryTransit(out IStateEnemyAI nextStateEnemyAI))
                {
                    continue;
                }

                stateEnemyAI = nextStateEnemyAI;
                UnsubscribeFromTransitionEvents();
                return true;
            }

            stateEnemyAI = null;
            return false;
        }

        public void HandleCompletedAction()
        {
            if (_needTransitAfterAction)
            {
                TransitNextState(_afterActionNextState);
            }
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                subManager.Enable();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                subManager.Disable();
            }
        }

        protected override void HandleStartCheckingConditions()
        {
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                subManager.StartCheckingConditions();
            }
        }

        protected override void HandleStopCheckingConditions()
        {
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                subManager.StopCheckingConditions();
            }
        }

        protected override void SubscribeOnTransitionEvents()
        {
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                subManager.NeedTransit += TransitNextState;
            }
        }

        protected override void UnsubscribeFromTransitionEvents()
        {
            foreach (ITransitionManagerEnemyAIWithDisabling subManager in AllSubManagers)
            {
                subManager.NeedTransit -= TransitNextState;
            }
        }

        private void TransitNextState(IStateEnemyAI nextState)
        {
            UnsubscribeFromTransitionEvents();
            NeedTransit?.Invoke(nextState);
        }
    }
}