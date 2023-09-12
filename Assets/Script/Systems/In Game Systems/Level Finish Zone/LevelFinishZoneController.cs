using System;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Interfaces;

namespace Systems.In_Game_Systems.Level_Finish_Zone
{
    public class LevelFinishZoneController : BoxColliderTriggerBase<IPlayer>, ILevelFinishZone
    {
        public event Action PlayerEnterFinishZone;

        protected override void OnRequiredObjectEnteringDetected()
        {
            base.OnRequiredObjectEnteringDetected();
            PlayerEnterFinishZone?.Invoke();
        }
    }
}