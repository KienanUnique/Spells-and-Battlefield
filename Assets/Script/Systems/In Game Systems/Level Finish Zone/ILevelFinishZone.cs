using System;

namespace Systems.In_Game_Systems.Level_Finish_Zone
{
    public interface ILevelFinishZone
    {
        event Action PlayerEnterFinishZone;
    }
}