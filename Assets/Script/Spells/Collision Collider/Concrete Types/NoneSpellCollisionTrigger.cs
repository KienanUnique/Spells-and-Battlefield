using System;
using UnityEngine;

namespace Spells.Collision_Collider.Concrete_Types
{
    public class NoneSpellCollisionTrigger : SpellCollisionTriggerBase
    {
        public override event Action<Collider> TriggerEntered;
    }
}