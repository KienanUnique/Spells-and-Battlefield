using System;

namespace Interfaces
{
    public interface ICharacterInformation : IInteractable
    {
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState { get; }
        public event Action<float> HitPointsCountChanged;
        public float HitPointCountRatio { get; }
    }
}