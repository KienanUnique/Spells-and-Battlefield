using System;
using Common;
using Common.Abstract_Bases.Character;

namespace Interfaces
{
    public interface ICharacterInformation
    {
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState { get; }
        public event Action<float> HitPointsCountChanged;
        public float HitPointCountRatio { get; }
    }
}