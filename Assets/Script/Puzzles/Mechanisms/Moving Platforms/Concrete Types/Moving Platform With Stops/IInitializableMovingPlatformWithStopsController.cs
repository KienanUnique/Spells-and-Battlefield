using System.Collections.Generic;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Triggers;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform_With_Stops
{
    public interface IInitializableMovingPlatformWithStopsController
    {
        public void Initialize(List<ITrigger> moveNextTriggers, List<ITrigger> movePreviousTriggers, IMovingPlatformDataForControllerBase dataForControllerBase);
    }
}