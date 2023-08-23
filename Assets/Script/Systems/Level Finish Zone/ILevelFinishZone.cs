using System;

namespace Systems.Level_Finish_Zone
{
    public interface ILevelFinishZone
    {
        event Action PlayerEnterFinishZone;
    }
}