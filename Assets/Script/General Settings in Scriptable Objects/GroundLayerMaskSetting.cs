using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Ground Layer Setting",
        menuName = "Spells and Battlefield/Settings/General Settings/Ground Layer Setting", order = 0)]
    public class GroundLayerMaskSetting : ScriptableObject
    {
        public LayerMask Mask => _groundMask;
        [SerializeField] private LayerMask _groundMask;
    }
}