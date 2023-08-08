﻿using System;
using System.Collections.Generic;
using Enemies.State_Machine.States;
using Enemies.State_Machine.Transition_Manager.Sub_Managers.Sub_Manager_Concrete_Types;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Manager
{
    [Serializable]
    public class MainTransitionManagerEnemyAI : TransitionManagerEnemyAIBase
    {
        [Space] [Header("After action state")] [SerializeField]
        private bool _needTransitAfterAction;

        [SerializeField] private StateEnemyAI _afterActionNextState;

        [Header("Single")] [SerializeField] private List<SingleTransitionSubManagerEnemyAI> _singleSubManagers;

        [Space] [Header("And")] [SerializeField]
        private List<ConjunctionMultipleTransitionSubManagerEnemyAI> _conjunctionSubManagers;

        [Space] [Header("Or")] [SerializeField]
        private List<DisjunctionMultipleTransitionSubManagerEnemyAI> _disjunctionSubManagers;

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

        public override event Action<IStateEnemyAI> NeedTransit;

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            foreach (var subManager in AllSubManagers)
            {
                subManager.Enable();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            foreach (var subManager in AllSubManagers)
            {
                subManager.Disable();
            }
        }

        protected override void HandleStartCheckingConditions()
        {
            foreach (var subManager in AllSubManagers)
            {
                subManager.StartCheckingConditions();
            }
        }

        protected override void HandleStopCheckingConditions()
        {
            foreach (var subManager in AllSubManagers)
            {
                subManager.StopCheckingConditions();
            }
        }

        protected override void SubscribeOnTransitionEvents()
        {
            foreach (var subManager in AllSubManagers)
            {
                subManager.NeedTransit += TransitNextState;
            }
        }

        protected override void UnsubscribeFromTransitionEvents()
        {
            foreach (var subManager in AllSubManagers)
            {
                subManager.NeedTransit -= TransitNextState;
            }
        }

        private void TransitNextState(IStateEnemyAI nextState)
        {
            UnsubscribeFromTransitionEvents();
            NeedTransit?.Invoke(nextState);
        }

        public void HandleCompletedAction()
        {
            if (_needTransitAfterAction)
            {
                TransitNextState(_afterActionNextState);
            }
        }
    }
}