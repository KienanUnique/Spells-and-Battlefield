using System;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Player;

namespace Systems.In_Game_Systems.Level_Finish_Zone
{
    public class LevelFinishZoneController : TriggerForInitializableObjectsBase<IPlayer>, ILevelFinishZone
    {
        public event Action PlayerEnterFinishZone;

        protected override void OnEnable()
        {
            base.OnEnable();
            RequiredObjectEnteringDetected += OnPlayerEnteringDetected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RequiredObjectEnteringDetected -= OnPlayerEnteringDetected;
        }

        private void OnPlayerEnteringDetected(IPlayer player)
        {
            PlayerEnterFinishZone?.Invoke();
        }
    }
}