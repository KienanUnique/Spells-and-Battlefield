using Common.Settings.Ground_Layer_Mask;
using UnityEngine;
using Zenject;

namespace Common.Abstract_Bases.Checkers.Ground_Checker
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundChecker : CheckerBase, IGroundChecker
    {
        private LayerMask _cashedGroundMask;

        [Inject]
        private void GetDependencies(IGroundLayerMaskSetting groundLayerMaskSetting)
        {
            _cashedGroundMask = groundLayerMaskSetting.Mask;
        }

        protected override LayerMask NeedObjectsMask => _cashedGroundMask;

        protected override void SpecialAwakeAction()
        {
        }
    }
}