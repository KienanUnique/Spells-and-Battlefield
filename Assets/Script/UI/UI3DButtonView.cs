using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI3DButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const string EmissionKeyword = "_EMISSION";
        [SerializeField] private Renderer _renderer;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _renderer.material.EnableKeyword(EmissionKeyword);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _renderer.material.DisableKeyword(EmissionKeyword);
        }
    }
}