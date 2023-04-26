using General_Settings_in_Scriptable_Objects;
using UnityEngine;

namespace Game_Managers
{
    public class SettingsProvider : Singleton<SettingsProvider>
    {
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private GroundLayerMaskSetting _groundLayerMaskSetting;
        [SerializeField] private PickableItemsSettings _pickableItemsSettings;
        [SerializeField] private UIAnimationSettings _uiAnimationSettings;

        public EnemySettings EnemySettings => _enemySettings;
        public GroundLayerMaskSetting GroundLayerMaskSetting => _groundLayerMaskSetting;
        public PickableItemsSettings PickableItemsSettings => _pickableItemsSettings;
        public UIAnimationSettings UIAnimationSettings => _uiAnimationSettings;

        protected override void SpecialAwakeAction()
        {
        }
    }
}