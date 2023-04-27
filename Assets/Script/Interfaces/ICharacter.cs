using System;

namespace Interfaces
{
    public interface ICharacter : IInteractable
    {
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState { get; }
        public void HandleHeal(int countOfHealthPoints);
        public void HandleDamage(int countOfHealthPoints);
        public event Action<CharacterState> StateChanged;
        public event Action<float> HitPointsCountChanged;
        public float HitPointCountRatio { get; }
    }
}