using Enemies.Target_Pathfinder.Settings;
using Enemies.Target_Pathfinder.Setup_Data;
using UnityEngine;

namespace Enemies.Target_Pathfinder.Provider
{
    [CreateAssetMenu(fileName = "Target Pathfinder Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory +
                   "Target Pathfinder Provider", order = 0)]
    public class TargetPathfinderProvider : ScriptableObject, ITargetPathfinderProvider
    {
        [SerializeField] private TargetPathfinderSettingsSection _settings;

        public ITargetPathfinder GetImplementationObject(ITargetPathfinderSetupData setupData)
        {
            return new TargetPathfinder(setupData, _settings);
        }
    }
}