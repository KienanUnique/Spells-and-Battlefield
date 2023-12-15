using System;
using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Controllers.Data_For_Controller;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Instant.Data_For_Controller
{
    [Serializable]
    public class
        DataForInstantSpellControllerProvider : DataForSpellControllerSerializableProviderBase<
            IDataForInstantSpellController>
    {
        [SerializeField] private List<InstantSpellScriptableObject> _nextSpellsOnFinish;
        [SerializeField] private SpellTriggerScriptableObject _spellMainTrigger;

        public override IDataForInstantSpellController GetImplementationObject(ISpellType type, ISpellMovement movement,
            ILookPointCalculator lookPointCalculator)
        {
            return new DataForInstantSpellController(movement, type, SpellAppliers, lookPointCalculator,
                _nextSpellsOnFinish, _spellMainTrigger.GetImplementationObject());
        }
    }
}