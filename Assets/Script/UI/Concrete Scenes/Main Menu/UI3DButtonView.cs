using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Concrete_Scenes.Main_Menu
{
    public class UI3DButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const string EmissionKeyword = "_EMISSION";
        [SerializeField] private Renderer _renderer;
        private Material _material;

        private void Awake()
        {
            _material = _renderer.material;
            DisableEmission();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EnableEmission();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DisableEmission();
        }

        private void EnableEmission()
        {
            _material.EnableKeyword(EmissionKeyword);
        }

        private void DisableEmission()
        {
            _material.DisableKeyword(EmissionKeyword);
        }
    }
}