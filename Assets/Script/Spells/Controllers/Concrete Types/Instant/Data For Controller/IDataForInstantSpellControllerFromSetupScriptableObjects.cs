using System.Collections.Generic;
using Spells.Controllers.Data_For_Controller;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Controllers.Concrete_Types.Instant.Data_For_Controller
{
    public interface
        IDataForInstantSpellControllerFromSetupScriptableObjects :
            IInitializableDataForSpellControllerFromSetupScriptableObjects
    {
        public IReadOnlyList<IInformationAboutInstantSpell> NextSpellsOnFinish { get; }
        public ISpellTrigger SpellMainTrigger { get; }
    }
}