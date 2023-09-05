using UnityEngine;

namespace Common.Settings.Ground_Layer_Mask
{
    public interface IGroundLayerMaskSetting
    {
        LayerMask Mask { get; }
    }
}