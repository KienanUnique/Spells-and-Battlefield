using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Common.Abstract_Bases.Checkers
{
    public interface IChecker
    {
        event Action<bool> ContactStateChanged;
        bool IsColliding { get; }
        ReadOnlyCollection<Collider> Colliders { get; }
    }
}