using Game_Managers;
using UnityEngine;

namespace Checkers
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundChecker : CheckerBase
    {
        private LayerMask _cashedGroundMask;
        protected override LayerMask NeedObjectsMask => _cashedGroundMask;

        protected override void SpecialAwakeAction()
        {
            _cashedGroundMask = SettingsProvider.Instance.GroundLayerMaskSetting.Mask;
        }
    }
}