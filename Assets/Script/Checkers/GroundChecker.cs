using General_Settings_in_Scriptable_Objects;
using UnityEngine;

namespace Checkers
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundChecker : CheckerBase
    {
        [SerializeField] private GroundLayerMaskSetting _groundMaskMaskSetting;
        protected override LayerMask NeedObjectsMask => _groundMaskMaskSetting.Mask;
    }
}