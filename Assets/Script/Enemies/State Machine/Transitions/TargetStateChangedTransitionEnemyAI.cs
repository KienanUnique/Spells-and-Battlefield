using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public class TargetStateChangedTransitionEnemyAI : TransitionEnemyAI
    {
        [SerializeField] private CharacterState _needState;
        private ICharacter _targetCharacter = null;
        private int _lastCashedTargetId;

        protected override void SpecialActionOnStartChecking()
        {
            TryUpdateCashedCharacter();
        }

        protected override void CheckConditions()
        {
            if (_targetCharacter != null && _targetCharacter.Id == StateMachineControllable.Target.Id &&
                _targetCharacter.CurrentCharacterState.Value == _needState)
            {
                InvokeTransitionEvent();
            }
            else if (_lastCashedTargetId != StateMachineControllable.Target.Id)
            {
                TryUpdateCashedCharacter();
            }
        }

        private void TryUpdateCashedCharacter()
        {
            if ((_targetCharacter == null || _targetCharacter.Id != StateMachineControllable.Target.Id) &&
                StateMachineControllable.Target is ICharacter targetCharacter)
            {
                _targetCharacter = targetCharacter;
            }

            _lastCashedTargetId = StateMachineControllable.Target.Id;
        }
    }
}