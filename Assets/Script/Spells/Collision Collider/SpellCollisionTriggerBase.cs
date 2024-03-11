using System;
using UnityEngine;

namespace Spells.Collision_Collider
{
    public abstract class SpellCollisionTriggerBase : MonoBehaviour, ISpellCollisionTrigger
    {
        public abstract event Action<Collider> TriggerEntered;
    }
}