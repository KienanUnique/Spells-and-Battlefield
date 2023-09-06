using System.Collections.Generic;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform
{
    public interface IInitializableMovingPlatformController
    {
        public void Initialize(List<IMechanismsTrigger> triggers,
            IMovingPlatformDataForControllerBase dataForControllerBase);
    }
}