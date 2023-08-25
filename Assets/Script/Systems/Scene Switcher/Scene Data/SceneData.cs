using UnityEngine;

namespace Systems.Scene_Switcher.Scene_Data
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.ScenesDirectory + "Scene Data",
        fileName = "Scene Data", order = 0)]
    public class SceneData : ScriptableObject, ISceneData
    {
        [SerializeField] private string _sceneName;
        public string SceneName => _sceneName;
    }
}