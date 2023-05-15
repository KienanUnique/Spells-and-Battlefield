using UnityEngine;

namespace Interfaces
{
    public interface IEnemyTarget : IIdHolder
    {
        public Transform MainTransform { get; }
    }
}