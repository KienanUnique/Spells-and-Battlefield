using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Data_For_Spell_Implementation
{
    public interface IDataForSpellImplementation
    {
        public Rigidbody SpellRigidbody { get; }
        public ICaster Caster { get; }
        public ICoroutineStarter CoroutineStarter { get; }
        public IReadonlyTransform CastPoint { get; }
        public ISpellTargetsDetector TargetsDetector { get; }
    }
}