using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Ground Layer Setting",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Ground Layer Setting", order = 0)]
    public class GroundLayerMaskSetting : ScriptableObject
    {
        public LayerMask Mask => _groundMask;
        [SerializeField] private LayerMask _groundMask;
    }
}