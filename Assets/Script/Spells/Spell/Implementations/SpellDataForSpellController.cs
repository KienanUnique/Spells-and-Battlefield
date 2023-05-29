﻿using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Spell.Implementations
{
    public class SpellDataForSpellController : ISpellDataForSpellController
    {
        public SpellDataForSpellController(List<ISpell> nextSpellsOnFinish, ISpellMovement spellObjectMovement,
            ISpellTrigger spellMainTrigger, List<ISpellApplier> spellAppliers)
        {
            NextSpellsOnFinish = nextSpellsOnFinish;
            SpellObjectMovement = spellObjectMovement;
            SpellMainTrigger = spellMainTrigger;
            SpellAppliers = spellAppliers;
        }

        public void Initialize(Rigidbody spellRigidbody, ICaster caster)
        {
            var spellImplementations = new List<ISpellImplementation>()
            {
                SpellObjectMovement,
                SpellMainTrigger
            };
            spellImplementations.AddRange(SpellAppliers);

            spellImplementations.ForEach(spellImplementation =>
                spellImplementation.Initialize(spellRigidbody, caster));
        }

        public List<ISpell> NextSpellsOnFinish { get; }
        public ISpellMovement SpellObjectMovement { get; }
        public ISpellTrigger SpellMainTrigger { get; }
        public List<ISpellApplier> SpellAppliers { get; }
    }
}