using System;
using Enemies.Look_Point_Calculator;
using Spells.Controllers.Data_For_Controller;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller
{
    [Serializable]
    public class
        DataForContinuousSpellControllerProvider : DataForSpellControllerSerializableProviderBase<
            IDataForContinuousSpellController>
    {
        [SerializeField] private float _durationInSeconds = 7f;

        public override IDataForContinuousSpellController GetImplementationObject(ISpellType type,
            ISpellMovement movement, ILookPointCalculator lookPointCalculator)
        {
            return new DataForContinuousSpellController(movement, type, SpellAppliers, lookPointCalculator,
                _durationInSeconds);
        }
    }
}