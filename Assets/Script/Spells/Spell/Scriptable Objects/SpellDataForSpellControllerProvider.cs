using System;
using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Spell.Scriptable_Objects
{
    [Serializable]
    public class SpellDataForSpellControllerProvider
    {
        [SerializeField] private SpellMovementScriptableObject _movement;
        [SerializeField] private List<SpellApplierScriptableObject> _appliers;
        [SerializeField] private SpellTriggerScriptableObject _mainTrigger;
        [SerializeField] private List<SpellScriptableObjectBase> _nextSpellsOnFinish;

        public ILookPointCalculator LookPointCalculator => SpellObjectMovement.GetLookPointCalculator();

        private List<ISpell> NextSpellsOnFinish
        {
            get
            {
                var iSpellDataForSpellController = new List<ISpell>();
                _nextSpellsOnFinish.ForEach(spell => iSpellDataForSpellController.Add(spell));
                return iSpellDataForSpellController;
            }
        }

        private ISpellMovementWithLookPointCalculator SpellObjectMovement => _movement.GetImplementationObject();
        private ISpellTrigger SpellMainTrigger => _mainTrigger.GetImplementationObject();

        private List<ISpellApplier> SpellAppliers
        {
            get
            {
                var iSpellAppliersList = new List<ISpellApplier>();
                _appliers.ForEach(spellApplier => iSpellAppliersList.Add(spellApplier.GetImplementationObject()));
                return iSpellAppliersList;
            }
        }

        public ISpellDataForSpellController GetImplementationObject(ISpellType spellType)
        {
            return new SpellDataForSpellController(NextSpellsOnFinish, SpellObjectMovement, SpellMainTrigger,
                SpellAppliers, spellType);
        }
    }
}