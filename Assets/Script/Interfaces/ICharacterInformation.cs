using System;
using Common.Abstract_Bases.Character;

namespace Interfaces
{
    public interface ICharacterInformationProvider
    {
        public CharacterState CurrentCharacterState { get; }
        public event Action<CharacterState> CharacterStateChanged;
        public event Action<float> HitPointsCountChanged;
        public float HitPointCountRatio { get; }
    }
}