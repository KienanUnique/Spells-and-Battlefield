using UnityEngine;

namespace UI.Loading_Window.View
{
    [CreateAssetMenu(fileName = "Loading Window Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Loading Window Settings", order = 0)]
    public class LoadingWindowSettings : ScriptableObject, ILoadingWindowSettings
    {
        [SerializeField] private float _rotateAnimationDurationSpeed = 0.3f;

        public float RotateAnimationDurationSpeed => _rotateAnimationDurationSpeed;
    }
}