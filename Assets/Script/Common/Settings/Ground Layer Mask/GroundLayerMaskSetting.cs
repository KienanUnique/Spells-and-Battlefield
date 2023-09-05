using UnityEngine;

namespace Common.Settings.Ground_Layer_Mask
{
    [CreateAssetMenu(fileName = "Ground Layer Setting",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Ground Layer Setting", order = 0)]
    public class GroundLayerMaskSetting : ScriptableObject, IGroundLayerMaskSetting
    {
        [SerializeField] private LayerMask _groundMask;
        public LayerMask Mask => _groundMask;
    }
}