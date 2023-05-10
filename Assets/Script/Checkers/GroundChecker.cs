using General_Settings_in_Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Checkers
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundChecker : CheckerBase
    {
        private LayerMask _cashedGroundMask;

        [Inject]
        private void Construct(GroundLayerMaskSetting groundLayerMaskSetting)
        {
            _cashedGroundMask = groundLayerMaskSetting.Mask;
        }

        protected override LayerMask NeedObjectsMask => _cashedGroundMask;

        protected override void SpecialAwakeAction()
        {
        }
    }
}