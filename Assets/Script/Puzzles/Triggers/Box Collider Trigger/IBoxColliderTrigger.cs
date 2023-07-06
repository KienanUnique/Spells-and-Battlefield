using System;
using UnityEngine;

namespace Puzzles.Triggers.Box_Collider_Trigger
{
    public interface IColliderTrigger
    {
        event Action<Collider> TriggerEnter;
        event Action<Collider> TriggerExit;
    }
}