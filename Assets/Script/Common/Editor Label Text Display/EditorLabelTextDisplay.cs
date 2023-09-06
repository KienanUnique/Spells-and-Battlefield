using UnityEditor;
using UnityEngine;

namespace Common.Editor_Label_Text_Display
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(ITextForEditorLabelProvider))]
    public class EditorLabelTextDisplay : MonoBehaviour
    {
        private const float MaximumRenderDistance = 100f;
        private readonly Vector3 _textOffset = Vector3.up * 2;
        private ITextForEditorLabelProvider _textProvider;

        private static bool IsSceneViewCameraInRange(Vector3 position, float distance)
        {
            Camera currentCamera = Camera.current;
            Vector3 cameraPosition = currentCamera.WorldToScreenPoint(position);
            return cameraPosition.x >= 0 &&
                   cameraPosition.x <= currentCamera.pixelWidth &&
                   cameraPosition.y >= 0 &&
                   cameraPosition.y <= currentCamera.pixelHeight &&
                   cameraPosition.z > 0 &&
                   cameraPosition.z < distance;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isEditor || !IsSceneViewCameraInRange(transform.position, MaximumRenderDistance))
            {
                return;
            }

            if (_textProvider != null || TryGetComponent(out _textProvider))
            {
                Handles.Label(transform.position + _textOffset, _textProvider.TextForEditorLabel);
            }
        }
    }
}