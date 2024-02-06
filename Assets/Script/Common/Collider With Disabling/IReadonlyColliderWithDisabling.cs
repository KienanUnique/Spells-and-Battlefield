using System;
using UnityEngine;

namespace Common.Collider_With_Disabling
{
    public interface IReadonlyColliderWithDisabling
    {
        public event Action<IReadonlyColliderWithDisabling, Collider> Disabled;
    }
}