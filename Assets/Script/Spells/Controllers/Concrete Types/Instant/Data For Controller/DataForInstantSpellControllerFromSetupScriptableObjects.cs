using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using Spells.Controllers.Data_For_Controller;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Controllers.Concrete_Types.Instant.Data_For_Controller
{
    public class DataForInstantSpellControllerFromSetupScriptableObjects :
        DataForSpellControllerFromSetupScriptableObjects,
        IDataForInstantSpellControllerFromSetupScriptableObjects
    {
        public DataForInstantSpellControllerFromSetupScriptableObjects(ISpellMovement spellObjectMovement,
            ISpellType spellType, IReadOnlyList<ISpellApplier> spellAppliers, ILookPointCalculator lookPointCalculator,
            IReadOnlyList<IInformationAboutInstantSpell> nextSpellsOnFinish, ISpellTrigger spellMainTrigger) : base(
            spellObjectMovement, spellType, spellAppliers, lookPointCalculator)
        {
            NextSpellsOnFinish = nextSpellsOnFinish;
            SpellMainTrigger = spellMainTrigger;
        }

        public IReadOnlyList<IInformationAboutInstantSpell> NextSpellsOnFinish { get; }
        public ISpellTrigger SpellMainTrigger { get; }

        protected override List<ISpellImplementation> SpellImplementations
        {
            get
            {
                List<ISpellImplementation> baseSpellImplementations = base.SpellImplementations;
                baseSpellImplementations.Add(SpellMainTrigger);
                return baseSpellImplementations;
            }
        }
    }
}