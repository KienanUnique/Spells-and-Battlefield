using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using Spells.Controllers.Data_For_Controller;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller
{
    public class DataForContinuousSpellController : DataForSpellController, IDataForContinuousSpellController
    {
        public DataForContinuousSpellController(ISpellMovement spellObjectMovement, ISpellType spellType,
            IReadOnlyList<ISpellApplier> spellAppliers, ILookPointCalculator lookPointCalculator,
            float durationInSeconds) : base(spellObjectMovement, spellType, spellAppliers, lookPointCalculator)
        {
            DurationInSeconds = durationInSeconds;
        }

        public float DurationInSeconds { get; }
    }
}