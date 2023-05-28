using UnityEngine;

namespace Interfaces
{
    public interface IEnemyTarget : ICharacter, IIdHolder
    {
        public Transform MainTransform { get; }
    }
}