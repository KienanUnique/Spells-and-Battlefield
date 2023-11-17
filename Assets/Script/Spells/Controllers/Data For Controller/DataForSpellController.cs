using System.Collections.Generic;
using Common.Interfaces;
using Enemies.Look_Point_Calculator;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Controllers.Data_For_Controller
{
    public abstract class DataForSpellController : IDataForSpellController
    {
        protected DataForSpellController(ISpellMovement spellObjectMovement, ISpellType spellType,
            IReadOnlyList<ISpellApplier> spellAppliers, ILookPointCalculator lookPointCalculator)
        {
            SpellObjectMovement = spellObjectMovement;
            SpellType = spellType;
            SpellAppliers = spellAppliers;
            LookPointCalculator = lookPointCalculator;
        }

        public void Initialize(Rigidbody spellRigidbody, ICaster caster, ICoroutineStarter coroutineStarter)
        {
            foreach (ISpellImplementation spellImplementation in SpellImplementations)
            {
                spellImplementation.Initialize(spellRigidbody, caster, coroutineStarter);
            }
        }

        public ISpellMovement SpellObjectMovement { get; }
        public ISpellType SpellType { get; }
        public IReadOnlyList<ISpellApplier> SpellAppliers { get; }
        public ILookPointCalculator LookPointCalculator { get; }

        protected virtual List<ISpellImplementation> SpellImplementations =>
            new List<ISpellImplementation>(SpellAppliers) {SpellObjectMovement};
    }
}