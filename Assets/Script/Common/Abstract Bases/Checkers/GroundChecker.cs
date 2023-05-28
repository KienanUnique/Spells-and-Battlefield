using Settings;
using UnityEngine;
using Zenject;

namespace Common.Abstract_Bases.Checkers
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