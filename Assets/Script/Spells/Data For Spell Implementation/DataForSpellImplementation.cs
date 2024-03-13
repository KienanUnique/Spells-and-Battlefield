using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Data_For_Spell_Implementation
{
    public class DataForSpellImplementation : IDataForSpellImplementation
    {
        public DataForSpellImplementation(Rigidbody spellRigidbody, Transform spellTransform, ICaster caster,
            ICoroutineStarter coroutineStarter, IReadonlyTransform castPoint, ISpellTargetsDetector targetsDetector)
        {
            SpellRigidbody = spellRigidbody;
            Caster = caster;
            CoroutineStarter = coroutineStarter;
            CastPoint = castPoint;
            TargetsDetector = targetsDetector;
            SpellTransform = spellTransform;
        }

        public Transform SpellTransform { get; }
        public Rigidbody SpellRigidbody { get; }
        public ICaster Caster { get; }
        public ICoroutineStarter CoroutineStarter { get; }
        public IReadonlyTransform CastPoint { get; }
        public ISpellTargetsDetector TargetsDetector { get; }
    }
}