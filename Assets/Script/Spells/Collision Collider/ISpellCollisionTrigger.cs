using System;
using UnityEngine;

namespace Spells.Collision_Collider
{
    public interface ISpellCollisionTrigger
    {
        event Action<Collider> TriggerEntered;
    }
}